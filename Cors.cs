using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace WcfHostService
{



    public class CorsEnabledMessageInspector : IDispatchMessageInspector

    {

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)

        {

            var httpRequest = (HttpRequestMessageProperty)request.Properties[HttpRequestMessageProperty.Name];



            return new

            {

                origin = httpRequest.Headers["Origin"],

                handlePreflight = httpRequest.Method.Equals("OPTIONS", StringComparison.InvariantCultureIgnoreCase)

            };

        }



        public void BeforeSendReply(ref Message reply, object correlationState)

        {

            var state = (dynamic)correlationState;

            // handle request preflight

            if (state.handlePreflight)

            {

                reply = Message.CreateMessage(MessageVersion.None, "PreflightReturn");



                var httpResponse = new HttpResponseMessageProperty();

                reply.Properties.Add(HttpResponseMessageProperty.Name, httpResponse);



                httpResponse.SuppressEntityBody = true;

                httpResponse.StatusCode = HttpStatusCode.OK;

            }



            // add allowed origin info

            var response = (HttpResponseMessageProperty)reply.Properties[HttpResponseMessageProperty.Name];

            response.Headers.Add("Access-Control-Allow-Origin", "*");

            response.Headers.Add("Access-Control-Allow-Methods", "POST,GET,PUT,DELETE,OPTIONS");

            response.Headers.Add("Access-Control-Allow-Headers", "X-Requested-With,Content-Type,Accept,Cache-Control,Pragma,Expires,Last-Modified,If-Modified-Since");

            response.Headers.Add("Access-Control-Expose-Headers", "If-Modified-Since");

            response.Headers.Add("Access-Control-Allow-Credentials", "true");

        }



    }

    public class CorsEnableBehavior : BehaviorExtensionElement, IEndpointBehavior

    {

        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)

        {



        }



        public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)

        {



        }



        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)

        {

            //var requiredHeaders = new Dictionary<string, string>();

            //requiredHeaders.Add("Access-Control-Allow-Origin", "*");

            //requiredHeaders.Add("Access-Control-Request-Method", "POST,GET,PUT,DELETE,OPTIONS");

            //requiredHeaders.Add("Access-Control-Allow-Headers", "X-Requested-With,Content-Type,Accept");

            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new CorsEnabledMessageInspector());

        }



        public void Validate(ServiceEndpoint endpoint)

        {



        }



        public override Type BehaviorType

        {

            get { return typeof(CorsEnableBehavior); }

        }



        protected override object CreateBehavior()

        {

            return new CorsEnableBehavior();

        }

    }
}
