using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Ontrack.AWS.Kinesis.Model;
using Ontrack.AWS.Kinesis;
using ServiceStack.ServiceClient.Web;
using ServiceStack.Text;


namespace Ontrack.AWS.Kinesis
{
    public class KinesisClient
    {
        #region Constructor
        public KinesisClient(string AccessKey, string SecretKey)
        {
            JsConfig.ExcludeTypeInfo = true;

            this.accessKey = AccessKey;
            this.secretKey = SecretKey;            
        }
        #endregion

        #region ListStreamsResponse ListStreams(ListStreamsRequest request)

        /// <summary>
        /// This operation returns an array of the names of all the streams that are associated with the AWS account 
        /// making the ListStreams request. 
        /// 
        /// More info: http://docs.aws.amazon.com/kinesis/latest/APIReference/API_ListStreams.html
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ListStreamsResponse ListStreams(ListStreamsRequest request)
        {
            return Invoke<ListStreamsResponse>(request, "ListStreams");
        }
        #endregion

        #region CreateStreamResponse CreateStream(CreateStreamRequest request)

        /// <summary>
        /// This operation adds a new Amazon Kinesis stream to your AWS account. 
        /// 
        /// More info: http://docs.aws.amazon.com/kinesis/latest/APIReference/API_CreateStream.html
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CreateStreamResponse CreateStream(CreateStreamRequest request)
        {
            return Invoke<CreateStreamResponse>(request, "CreateStream");
        }
        #endregion

        #region DeleteStreamResponse DeleteStream(DeleteStreamRequest request)

        /// <summary>
        /// This operation deletes a stream and all of its shards and data. 
        /// 
        /// More info: http://docs.aws.amazon.com/kinesis/latest/APIReference/API_DeleteStream.html
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DeleteStreamResponse DeleteStream(DeleteStreamRequest request)
        {
            return Invoke<DeleteStreamResponse>(request, "DeleteStream");
        }
        #endregion

        #region DescribeStreamResponse DescribeStream(DescribeStreamRequest request)

        /// <summary>
        /// This operation returns the following information about the stream: the current status of the 
        /// stream, the stream Amazon Resource Name (ARN), and an array of shard objects that comprise the stream.
        /// 
        /// More info: http://docs.aws.amazon.com/kinesis/latest/APIReference/API_DescribeStream.html
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DescribeStreamResponse DescribeStream(DescribeStreamRequest request)
        {
            return Invoke<DescribeStreamResponse>(request, "DescribeStream");
        }
        #endregion

        #region MergeShardsResponse MergeShards(MergeShardsRequest request)

        /// <summary>
        /// This operation merges two adjacent shards in a stream and combines them into a 
        /// single shard to reduce the stream's capacity to ingest and transport data.
        /// More info: http://docs.aws.amazon.com/kinesis/latest/APIReference/API_MergeShards.html
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public MergeShardsResponse MergeShards(MergeShardsRequest request)
        {
            return Invoke<MergeShardsResponse>(request, "MergeShards");
        }
        #endregion

        #region SplitShardResponse SplitShard(SplitShardRequest request)

        /// <summary>
        /// This operation splits a shard into two new shards in the stream, to 
        /// increase the stream's capacity to ingest and transport data.
        /// More info: http://docs.aws.amazon.com/kinesis/latest/APIReference/API_SplitShard.html
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public SplitShardResponse SplitShard(SplitShardRequest request)
        {
            return Invoke<SplitShardResponse>(request, "SplitShard");
        }
        #endregion

        #region GetShardIteratorResponse GetShardIterator(GetShardIteratorRequest request)

        /// <summary>
        /// This operation returns a shard iterator in ShardIterator.
        /// More info: http://docs.aws.amazon.com/kinesis/latest/APIReference/API_GetShardIterator.html
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public GetShardIteratorResponse GetShardIterator(GetShardIteratorRequest request)
        {
            return Invoke<GetShardIteratorResponse>(request, "GetShardIterator");
        }
        #endregion

        #region PutRecordResponse PutRecord(PutRecordRequest request)

        /// <summary>
        /// This operation puts a data record into an Amazon Kinesis stream from a producer.
        /// More info: http://docs.aws.amazon.com/kinesis/latest/APIReference/API_PutRecord.html
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PutRecordResponse PutRecord(PutRecordRequest request)
        {
            return Invoke<PutRecordResponse>(request, "PutRecord");
        }
        #endregion

        #region GetRecordsResponse GetRecords(GetRecordsRequest request)

        /// <summary>
        /// This operation returns one or more data records from a shard. 
        /// More info: http://docs.aws.amazon.com/kinesis/latest/APIReference/API_GetRecords.html
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public GetRecordsResponse GetRecords(GetRecordsRequest request)
        {
            //kinesisUrl = "http://kinesis.us-east-1.amazonaws.com";
            return Invoke<GetRecordsResponse>(request, "GetRecords");
        }
        #endregion

        #region private utility methods

        private T Invoke<T>(Request request, string method) where T : Response, new()
        {
            if (!request.IsValidRequest())
                throw new Exception("Invalid request.");

            var payload = request.ToJson();
            try
            {
                var response = kinesisUrl.PostJsonToUrl(payload, requestFilter: webReq =>
                {
                    SignRequestWithSignatureVersion4(webReq, method, payload);
                }).FromJson<T>();
                return response;
            }
            catch (System.Net.WebException we)
            {
                // TODO: Implement transient fault, retry, pause between retries, etc. strategies
                using (HttpWebResponse httpErrorResponse = we.Response as HttpWebResponse)
                {
                    var statusCode = httpErrorResponse.StatusCode;
                    using (var sr = new StreamReader(httpErrorResponse.GetResponseStream()))
                    {
                        var detail = sr.ReadToEnd().FromJson<JsonObject>(); ;
                        var awsExceptionType = detail.Get("__type");
                        var awsExceptionMessage = detail.Get("message");
                        throw MakeAwsException(awsExceptionType, awsExceptionMessage);
                    }
                }
            }
        }

        private Exception MakeAwsException(string awsExceptionType, string awsExceptionMessage)
        {
            switch (awsExceptionType)
            {
                // Method call errors
                case ("InvalidArgumentException"):
                    return new InvalidArgumentException(awsExceptionMessage);
                case ("LimitExceededException"):
                    return new LimitExceededException(awsExceptionMessage);
                case ("ResourceInUseException"):
                    return new ResourceInUseException(awsExceptionMessage);
                case ("ExpiredIteratorException"):
                    return new ExpiredIteratorException(awsExceptionMessage);
                case ("ProvisionedThroughputExceededException"):
                    return new ProvisionedThroughputExceededException(awsExceptionMessage);
                case ("ResourceNotFoundException"):
                    return new ResourceNotFoundException(awsExceptionMessage);
                
                // Common errors
                case ("InvalidSignatureException"):
                    return new InvalidSignatureException(awsExceptionMessage);
                case ("IncompleteSignatureException"):
                    return new IncompleteSignatureException(awsExceptionMessage);
                case ("InternalFailureException"):
                    return new InternalFailureException(awsExceptionMessage);
                case ("InvalidActionException"):
                    return new InvalidActionException(awsExceptionMessage);
                case ("InvalidClientTokenIdException"):
                    return new InvalidClientTokenIdException(awsExceptionMessage);
                case ("InvalidParameterCombinationException"):
                    return new InvalidParameterCombinationException(awsExceptionMessage);
                case ("InvalidParameterValueException"):
                    return new InvalidParameterValueException(awsExceptionMessage);
                case ("InvalidQueryParameterException"):
                    return new InvalidQueryParameterException(awsExceptionMessage);
                case ("MalformedQueryStringException"):
                    return new MalformedQueryStringException(awsExceptionMessage);
                case ("MissingActionException"):
                    return new MissingActionException(awsExceptionMessage);
                case ("MissingAuthenticationTokenException"):
                    return new MissingAuthenticationTokenException(awsExceptionMessage);
                case ("MissingParameterException"):
                    return new MissingParameterException(awsExceptionMessage);
                case ("OptInRequiredException"):
                    return new OptInRequiredException(awsExceptionMessage);
                case ("RequestExpiredException"):
                    return new RequestExpiredException(awsExceptionMessage);
                case ("ServiceUnavailableException"):
                    return new ServiceUnavailableException(awsExceptionMessage);
                case ("ThrottlingException"):
                    return new ThrottlingException(awsExceptionMessage);
                case ("ValidationErrorException"):
                    return new ValidationErrorException(awsExceptionMessage);                    

                default:
                    return new Exception(awsExceptionType + ": " + awsExceptionMessage);
            }
        }

        #endregion

        #region Signing Code

        private void SignRequestWithSignatureVersion4(System.Net.HttpWebRequest request, string method, string payload)
        {
            var dt = DateTime.UtcNow;
            var longDate = dt.ToString(ISO8601BasicDateTimeFormat, CultureInfo.InvariantCulture);
            var shortDate = dt.ToString("yyyyMMdd", CultureInfo.InvariantCulture);

            var headers = new Dictionary<string, string>();
            headers.Add("content-length", payload.Length.ToString());
            headers.Add("content-type", "application/x-amz-json-1.1");
            headers.Add("host", "kinesis.us-east-1.amazonaws.com");
            headers.Add("x-amz-date", longDate);
            headers.Add("x-amz-target", "Kinesis_" + apiVersion + "." + method);

            var signingKey = getSignatureKey(secretKey, shortDate, "us-east-1", "kinesis");
            var canonicalRequest = createCanonicalRequest(headers, payload);
            var signedRequest = ComputeSHA256Hash(canonicalRequest);
            var stringToSign = string.Format("AWS4-HMAC-SHA256\n{0}\n{1}/us-east-1/kinesis/aws4_request\n{2}",
                longDate, shortDate, signedRequest);
            var signature = toHex(HmacSHA256(stringToSign, signingKey), true);

            var authorization = string.Format("AWS4-HMAC-SHA256 Credential={0}/{1}/us-east-1/kinesis/aws4_request, SignedHeaders={2}, Signature={3}",
                accessKey,
                shortDate,
                string.Join(";", headers.Keys),
                signature);

            request.Headers["Authorization"] = authorization;
            request.Headers["x-amz-date"] = longDate;
            request.Headers["x-amz-target"] = "Kinesis_" + apiVersion + "." + method;
            request.ContentType = "application/x-amz-json-1.1";
            _addWithoutValidateHeadersMethod.Invoke(request.Headers, new[] { "Host", "kinesis.us-east-1.amazonaws.com" });
        }

        private byte[] HmacSHA256(String data, byte[] key)
        {
            String algorithm = "HmacSHA256";
            KeyedHashAlgorithm kha = KeyedHashAlgorithm.Create(algorithm);
            kha.Key = key;

            return kha.ComputeHash(Encoding.UTF8.GetBytes(data));
        }

        private string ComputeSHA256Hash(string data)
        {
            return toHex(ComputeSHA256Hash(Encoding.UTF8.GetBytes(data)), true);
        }

        private byte[] ComputeSHA256Hash(byte[] data)
        {
            HashAlgorithm hashAlgorithm = null;
            try
            {
                hashAlgorithm = HashAlgorithm.Create("SHA-256");
            }
            catch (Exception) // Managed Hash Provider is not FIPS compliant.
            {
                hashAlgorithm = new SHA256CryptoServiceProvider();
            }
            return hashAlgorithm.ComputeHash(data);
        }

        private byte[] getSignatureKey(String key, String shortDate, string region, string service)
        {
            byte[] kSecret = Encoding.UTF8.GetBytes(("AWS4" + key).ToCharArray());
            byte[] kDate = HmacSHA256(shortDate, kSecret);
            byte[] kRegion = HmacSHA256(region, kDate);
            byte[] kService = HmacSHA256(service, kRegion);
            byte[] kSigning = HmacSHA256("aws4_request", kService);

            return kSigning;
        }

        private string createCanonicalRequest(Dictionary<string, string> headers, string payload)
        {
            var canonicalHeaders = new SortedDictionary<string, string>();
            foreach (var header in headers)
                canonicalHeaders.Add(header.Key.ToLowerInvariant(), header.Value.Trim());

            var canonicalRequest = new StringBuilder();
            canonicalRequest.AppendFormat("{0}\n", "POST");
            canonicalRequest.AppendFormat("{0}\n", "/");
            canonicalRequest.AppendFormat("{0}\n", "");
            foreach (var header in canonicalHeaders)
                canonicalRequest.AppendFormat("{0}:{1}\n", header.Key, header.Value);
            canonicalRequest.AppendFormat("{0}\n", "");
            canonicalRequest.AppendFormat("{0}\n", string.Join(";", canonicalHeaders.Keys));
            canonicalRequest.Append(toHex(ComputeSHA256Hash(Encoding.UTF8.GetBytes(payload)), true));

            return canonicalRequest.ToString();
        }

        private string toHex(byte[] data, bool lowercase)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString(lowercase ? "x2" : "X2", CultureInfo.InvariantCulture));
            }

            return sb.ToString();
        }
        
        #endregion

        #region Private vars

        private string kinesisUrl = "https://kinesis.us-east-1.amazonaws.com";
        private string apiVersion = "20131202";
        private const string ISO8601BasicDateTimeFormat = "yyyyMMddTHHmmssZ";
        private string accessKey;
        private string secretKey;
        private static System.Reflection.MethodInfo _addWithoutValidateHeadersMethod =
            typeof(WebHeaderCollection).GetMethod("AddWithoutValidate", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

        #endregion

    }
}
