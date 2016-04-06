using System;
using System.Activities;
using System.IO;
using System.Net;

namespace CreateAsyncHttpGetActivity
{
    public class AsyncHttpGetActivity : AsyncCodeActivity<string>
    {
        public InArgument<string> Uri { get; set; }

        protected override IAsyncResult BeginExecute(AsyncCodeActivityContext context, AsyncCallback callback, object state)
        {
            var request = HttpWebRequest.Create(Uri.Get(context));
            context.UserState = request;
            return request.BeginGetResponse(callback, state);
        }

        protected override string EndExecute(AsyncCodeActivityContext context, IAsyncResult result)
        {
            var request = context.UserState as WebRequest;
            using (var response = request.EndGetResponse(result))
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}