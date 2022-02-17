using System.Collections;
using System.Collections.Generic;

using Amazon;
using Amazon.CognitoIdentity;
using Amazon.S3;
using Amazon.S3.Transfer;

using UnityEngine;

namespace Vanilla.Catalogue
{

	public static class AWSApp
	{

		internal const string c_S3BucketName = "mhoplaceappcontent";

//		internal const string c_S3BucketName = "story-machine";

		private const string identityPoolId = "ap-southeast-2:b5d45675-818f-4635-86c6-c8f3915280c2";

//		private const string identityPoolId = "ap-southeast-2:2473494b-847f-4a44-b944-e57e51efac86";

		internal static readonly RegionEndpoint regionEndpoint = RegionEndpoint.APSoutheast2;

		private static readonly CognitoAWSCredentials credentials = new(identityPoolId: identityPoolId,
		                                                                region: regionEndpoint);

		internal static readonly AmazonS3Client client = new(credentials: credentials,
		                                                     region: regionEndpoint);

		internal static readonly TransferUtility transferUtility = new(s3Client: client);

	}

}