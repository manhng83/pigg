//-----------------------------------------------------------------------
// <copyright file="AuthController.cs" company="Andrew Arnott">
//     Copyright (c) Andrew Arnott. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace MvcRelyingParty.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using DotNetOpenAuth.Messaging;
    using DotNetOpenAuth.OpenId;
    using DotNetOpenAuth.OpenId.RelyingParty;
    using System.Web.Security;
    using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
    using PiggMvc3App.Controllers;
    //using DotNetOpenAuth.ApplicationBlock;
    //using DotNetOpenAuth.ApplicationBlock.Facebook;
    using System.Configuration;
    //using DotNetOpenAuth.OAuth2;
    using DotNetOpenAuth.OAuth;
    using DotNetOpenAuth.OAuth.ChannelElements;
    using DotNetOpenAuth.OAuth.Messages;
    using DotNetOpenAuth.OpenId.Extensions.OAuth;
    using DotNetOpenAuth.ApplicationBlock;

    public class AuthController : MyBaseController
    {

        /// <summary>
        /// Gets the OpenID relying party to use for logging users in.
        /// </summary>
        public IOpenIdRelyingParty RelyingParty = new OpenIdRelyingPartyService();

        private Uri PrivacyPolicyUrl
        {
            get
            {
                return Url.ActionFull("PrivacyPolicy", "Home");
            }
        }

        /// <summary>
        /// Performs discovery on a given identifier.
        /// </summary>
        /// <param name="identifier">The identifier on which to perform discovery.</param>
        /// <returns>The JSON result of discovery.</returns>
        public ActionResult Discover(string identifier)
        {
            if (!this.Request.IsAjaxRequest())
            {
                throw new InvalidOperationException();
            }

            return RelyingParty.AjaxDiscovery(
                identifier,
                Realm.AutoDetect,
                Url.ActionFull("PopUpReturnTo"),
                this.PrivacyPolicyUrl);
        }

        /// <summary>
        /// Prepares a web page to help the user supply his login information.
        /// </summary>
        /// <returns>The action result.</returns>
        public ActionResult LogOn()
        {
            return View();
        }

        /// <summary>
        /// Prepares a web page to help the user supply his login information.
        /// </summary>
        /// <returns>The action result.</returns>
        public ActionResult LogOnPopUp()
        {
            return View();
        }

        /// <summary>
        /// Handles the positive assertion that comes from Providers to Javascript running in the browser.
        /// </summary>
        /// <returns>The action result.</returns>
        /// <remarks>
        /// This method instructs ASP.NET MVC to <i>not</i> validate input
        /// because some OpenID positive assertions messages otherwise look like
        /// hack attempts and result in errors when validation is turned on.
        /// </remarks>
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post), ValidateInput(false)]
        public ActionResult PopUpReturnTo()
        {
            return RelyingParty.ProcessAjaxOpenIdResponse();
        }

        /// <summary>
        /// Handles the positive assertion that comes from Providers.
        /// </summary>
        /// <param name="openid_openidAuthData">The positive assertion obtained via AJAX.</param>
        /// <returns>The action result.</returns>
        /// <remarks>
        /// This method instructs ASP.NET MVC to <i>not</i> validate input
        /// because some OpenID positive assertions messages otherwise look like
        /// hack attempts and result in errors when validation is turned on.
        /// </remarks>
        [HttpPost, ValidateInput(false)]
        public ActionResult LogOnPostAssertion(string openid_openidAuthData)
        {
            IAuthenticationResponse response;
            if (!string.IsNullOrEmpty(openid_openidAuthData))
            {
                var auth = new Uri(openid_openidAuthData);
                var headers = new WebHeaderCollection();
                foreach (string header in Request.Headers)
                {
                    headers[header] = Request.Headers[header];
                }

                // Always say it's a GET since the payload is all in the URL, even the large ones.
                HttpRequestInfo clientResponseInfo = HttpRequestInfo.Create("GET", auth, null, headers) as HttpRequestInfo;
                response = RelyingParty.GetResponse(clientResponseInfo);
            }
            else
            {
                response = RelyingParty.GetResponse();
            }
            if (response != null)
            {
                switch (response.Status)
                {
                    case AuthenticationStatus.Authenticated:
                        string alias = response.FriendlyIdentifierForDisplay;
                        string email = string.Empty;
                        var sreg = response.GetExtension<ClaimsResponse>();
                        if (sreg != null && sreg.MailAddress != null)
                        {
                            alias = sreg.MailAddress.User;
                            email = sreg.Email;
                        }
                        if (sreg != null && !string.IsNullOrEmpty(sreg.FullName))
                        {
                            alias = sreg.FullName;
                        }

                        FormsAuthenticationTicket authTicket = new
                            FormsAuthenticationTicket(1, //version
                            response.ClaimedIdentifier, // user name
                            DateTime.Now,             //creation
                            DateTime.Now.AddMinutes(30), //Expiration
                            false, //Persistent
                            alias);

                        string encTicket = FormsAuthentication.Encrypt(authTicket);

                        this.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                        createUserIfNotExist(alias, email);
                        SetUserIdentityForView(alias);
                        string returnUrl = Request.Form["returnUrl"];
                        if (!String.IsNullOrEmpty(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    case AuthenticationStatus.Canceled:
                        ModelState.AddModelError("OpenID", "It looks like you canceled login at your OpenID Provider.");
                        break;
                    case AuthenticationStatus.Failed:
                        ModelState.AddModelError("OpenID", response.Exception.Message);
                        break;
                }
            }

            // If we're to this point, login didn't complete successfully.
            // Show the LogOn view again to show the user any errors and
            // give another chance to complete login.
            return View("LogOn");
        }

        public ActionResult OpenIdTwitter()
        {
            var twitter = new WebConsumer(TwitterConsumer.ServiceDescription, this.TokenManager);
            // Is Twitter calling back with authorization?
            var accessTokenResponse = twitter.ProcessUserAuthorization();
            if (accessTokenResponse != null)
            {
                this.AccessToken = accessTokenResponse.AccessToken;
                FormsAuthentication.SetAuthCookie("<em>user_id</em>", true);
                SetUserIdentityForView(accessTokenResponse.ExtraData["user_id"]);
                createUserIfNotExist(accessTokenResponse.ExtraData["user_id"], accessTokenResponse.ExtraData["screen_name"].ToString());
            }
            else if (this.AccessToken == null)
            {
                // If we don't yet have access, immediately request it.
                twitter.Channel.Send(twitter.PrepareRequestUserAuthorization());
            }
            string returnUrl = Request.Form["returnUrl"];
            if (!String.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            // If we're to this point, login didn't complete successfully.
            // Show the LogOn view again to show the user any errors and
            // give another chance to complete login.
            return View("LogOn");
        }

        private string AccessToken
        {
            get { return (string)Session["TwitterAccessToken"]; }
            set { Session["TwitterAccessToken"] = value; }
        }

        public void SetUserIdentityForView(string username)
        {

            ViewData["UserName"] = username;
        }
        //public ActionResult OpenIdFacebook()
        //{
        //    IAuthorizationState authorization = client.ProcessUserAuthorization();
        //    if (authorization == null)
        //    {
        //        // Kick off authorization request
        //        client.RequestUserAuthorization();
        //    }
        //    else
        //    {
        //        var request = WebRequest.Create("https://graph.facebook.com/me?access_token=" + Uri.EscapeDataString(authorization.AccessToken));
        //        using (var response = request.GetResponse())
        //        {
        //            using (var responseStream = response.GetResponseStream())
        //            {
        //                var graph = FacebookGraph.Deserialize(responseStream);
        //                var nText = HttpUtility.HtmlEncode(graph.Name);
        //            }
        //        }
        //    }
        //    return View("LogOn");
        //}

        //private static readonly FacebookClient client = new FacebookClient
        //{
        //    ClientIdentifier = ConfigurationManager.AppSettings["facebookAppID"],
        //    ClientSecret = ConfigurationManager.AppSettings["facebookAppSecret"],
        //};

        [NonAction]
        private MembershipCreateStatus createUserIfNotExist(string userName, string email)
        {
            MembershipCreateStatus status;
            var loggingUser = Membership.GetUser(userName);
            if (loggingUser == null)
            {
                // Is the first time that this user try to log in using openId. Create it                              
                loggingUser = Membership.CreateUser(userName, PiggMvc3App.Helpers.Constants.OpenIdPassword, email, null, null, true, out status);
            }
            else
                status = MembershipCreateStatus.Success;
            return status;
        }


        private InMemoryTokenManager TokenManager
        {
            get
            {
                var tokenManager = (InMemoryTokenManager)Session["TwitterTokenManager"];
                if (tokenManager == null)
                {
                    string consumerKey = ConfigurationManager.AppSettings["twitterConsumerKey"];
                    string consumerSecret = ConfigurationManager.AppSettings["twitterConsumerSecret"];
                    if (!string.IsNullOrEmpty(consumerKey))
                    {
                        tokenManager = new InMemoryTokenManager(consumerKey, consumerSecret);
                        Session["TwitterTokenManager"] = tokenManager;
                    }
                }

                return tokenManager;
            }
        }
    }

    public  class InMemoryTokenManager : IConsumerTokenManager, IOpenIdOAuthTokenManager
    {
        private Dictionary<string, string> tokensAndSecrets = new Dictionary<string, string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryTokenManager"/> class.
        /// </summary>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        public InMemoryTokenManager(string consumerKey, string consumerSecret)
        {
            if (String.IsNullOrEmpty(consumerKey))
            {
                throw new ArgumentNullException("consumerKey");
            }

            this.ConsumerKey = consumerKey;
            this.ConsumerSecret = consumerSecret;
        }

        /// <summary>
        /// Gets the consumer key.
        /// </summary>
        /// <value>The consumer key.</value>
        public string ConsumerKey { get; private set; }

        /// <summary>
        /// Gets the consumer secret.
        /// </summary>
        /// <value>The consumer secret.</value>
        public string ConsumerSecret { get; private set; }

        #region ITokenManager Members

        /// <summary>
        /// Gets the Token Secret given a request or access token.
        /// </summary>
        /// <param name="token">The request or access token.</param>
        /// <returns>
        /// The secret associated with the given token.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown if the secret cannot be found for the given token.</exception>
        public string GetTokenSecret(string token)
        {
            return this.tokensAndSecrets[token];
        }

        /// <summary>
        /// Stores a newly generated unauthorized request token, secret, and optional
        /// application-specific parameters for later recall.
        /// </summary>
        /// <param name="request">The request message that resulted in the generation of a new unauthorized request token.</param>
        /// <param name="response">The response message that includes the unauthorized request token.</param>
        /// <exception cref="ArgumentException">Thrown if the consumer key is not registered, or a required parameter was not found in the parameters collection.</exception>
        /// <remarks>
        /// Request tokens stored by this method SHOULD NOT associate any user account with this token.
        /// It usually opens up security holes in your application to do so.  Instead, you associate a user
        /// account with access tokens (not request tokens) in the <see cref="ExpireRequestTokenAndStoreNewAccessToken"/>
        /// method.
        /// </remarks>
        public void StoreNewRequestToken(UnauthorizedTokenRequest request, ITokenSecretContainingMessage response)
        {
            this.tokensAndSecrets[response.Token] = response.TokenSecret;
        }

        /// <summary>
        /// Deletes a request token and its associated secret and stores a new access token and secret.
        /// </summary>
        /// <param name="consumerKey">The Consumer that is exchanging its request token for an access token.</param>
        /// <param name="requestToken">The Consumer's request token that should be deleted/expired.</param>
        /// <param name="accessToken">The new access token that is being issued to the Consumer.</param>
        /// <param name="accessTokenSecret">The secret associated with the newly issued access token.</param>
        /// <remarks>
        /// 	<para>
        /// Any scope of granted privileges associated with the request token from the
        /// original call to <see cref="StoreNewRequestToken"/> should be carried over
        /// to the new Access Token.
        /// </para>
        /// 	<para>
        /// To associate a user account with the new access token,
        /// <see cref="System.Web.HttpContext.User">HttpContext.Current.User</see> may be
        /// useful in an ASP.NET web application within the implementation of this method.
        /// Alternatively you may store the access token here without associating with a user account,
        /// and wait until <see cref="WebConsumer.ProcessUserAuthorization()"/> or
        /// <see cref="DesktopConsumer.ProcessUserAuthorization(string, string)"/> return the access
        /// token to associate the access token with a user account at that point.
        /// </para>
        /// </remarks>
        public void ExpireRequestTokenAndStoreNewAccessToken(string consumerKey, string requestToken, string accessToken, string accessTokenSecret)
        {
            this.tokensAndSecrets.Remove(requestToken);
            this.tokensAndSecrets[accessToken] = accessTokenSecret;
        }

        /// <summary>
        /// Classifies a token as a request token or an access token.
        /// </summary>
        /// <param name="token">The token to classify.</param>
        /// <returns>Request or Access token, or invalid if the token is not recognized.</returns>
        public TokenType GetTokenType(string token)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IOpenIdOAuthTokenManager Members

        /// <summary>
        /// Stores a new request token obtained over an OpenID request.
        /// </summary>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="authorization">The authorization message carrying the request token and authorized access scope.</param>
        /// <remarks>
        /// 	<para>The token secret is the empty string.</para>
        /// 	<para>Tokens stored by this method should be short-lived to mitigate
        /// possible security threats.  Their lifetime should be sufficient for the
        /// relying party to receive the positive authentication assertion and immediately
        /// send a follow-up request for the access token.</para>
        /// </remarks>
        public void StoreOpenIdAuthorizedRequestToken(string consumerKey, AuthorizationApprovedResponse authorization)
        {
            this.tokensAndSecrets[authorization.RequestToken] = String.Empty;
        }

        #endregion
    }
}
