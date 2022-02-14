using System;

using Amazon;
using Amazon.CognitoIdentity;
using Amazon.S3;
using Amazon.S3.Transfer;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.StringFormatting;

using static UnityEngine.Debug;

namespace Vanilla.MediaLibrary.Samples.AWS
{

	[Serializable]
	public abstract class AWSCatalogue<I> : BaseCatalogue<I>
		where I : AWSCatalogueItem
	{

		protected CognitoAWSCredentials _credentials;
		public    CognitoAWSCredentials AWSCredentials => _credentials;

		protected AmazonS3Client _client;
		public    AmazonS3Client AWSClient => _client;

		protected TransferUtility _transferUtility;
		public    TransferUtility AWSTransferUtility => _transferUtility;

		[SerializeField]
		private string _bucket;

		[SerializeField]
		private string _id;

		[SerializeField]
		private string _region;


		public override UniTask Initialize()
		{
			Log(message: "Sample catalogue handling initialization...");

			_credentials = new CognitoAWSCredentials(identityPoolId: _id.X(twist: K()),
			                                         region: RegionEndpoint.GetBySystemName(systemName: _region.X(twist: K())));

			_client = new AmazonS3Client(credentials: _credentials,
			                             region: RegionEndpoint.GetBySystemName(systemName: _region.X(twist: K())));

			_transferUtility = new TransferUtility(s3Client: _client);

			Log(message: "Done!");

			return default;
		}

	}

}