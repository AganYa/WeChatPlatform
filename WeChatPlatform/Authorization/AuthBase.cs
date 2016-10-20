﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeChatPlatform.Entity;

namespace WeChatPlatform.Authorization
{
    public class AuthBase
    {
        /// <summary>
        /// 基础支持：获取access_token。
        /// </summary>
        /// <returns></returns>
        public static BaseAccessTokenEntity GetBaseAccessToken()
        {
            var url
                = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}"
                , Env.AppId, Env.Secret);

            string jsonContent = Encoding.UTF8.GetString(new WebClient().DownloadData(url));
            if (string.IsNullOrEmpty(jsonContent))
                return null;

            var token = JsonHelper.DeserializeJsonToObject<BaseAccessTokenEntity>(jsonContent);

            return token;
        }

        /// <summary>
        /// 基础支持：获取用户信息。
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="openid"></param>
        /// <returns></returns>
        public static BaseWeChatUserEntity GetBaseWeChatUserInfo(string access_token, string openid)
        {
            string url
                = string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN"
                , access_token, openid);

            string jsonContent = Encoding.UTF8.GetString(new WebClient().DownloadData(url));
            if (string.IsNullOrEmpty(jsonContent))
                return null;

            var user = JsonHelper.DeserializeJsonToObject<BaseWeChatUserEntity>(jsonContent);

            return user;
        }
    }
}
