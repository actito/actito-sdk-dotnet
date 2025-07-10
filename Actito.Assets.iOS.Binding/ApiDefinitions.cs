using System;
using Foundation;
using ObjCRuntime;

namespace ActitoSdk.Assets.iOS.Binding
{
	// @interface ActitoAsset : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC19ActitoAssetsBinding11ActitoAsset")]
	[DisableDefaultCtor]
	interface ActitoAsset : INativeObject
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull title;
		[Export ("title")]
		string Title { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable assetDescription;
		[NullAllowed, Export ("assetDescription")]
		string AssetDescription { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable key;
		[NullAllowed, Export ("key")]
		string Key { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable url;
		[NullAllowed, Export ("url")]
		string Url { get; }

		// @property (readonly, nonatomic, strong) ActitoAssetButton * _Nullable button;
		[NullAllowed, Export ("button", ArgumentSemantic.Strong)]
		ActitoAssetButton Button { get; }

		// @property (readonly, nonatomic, strong) ActitoAssetMetaData * _Nullable metaData;
		[NullAllowed, Export ("metaData", ArgumentSemantic.Strong)]
		ActitoAssetMetaData MetaData { get; }

		// @property (readonly, copy, nonatomic) NSDictionary<NSString *,id> * _Nonnull extra;
		[Export ("extra", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> Extra { get; }

		// -(instancetype _Nonnull)initWithTitle:(NSString * _Nonnull)title assetDescription:(NSString * _Nullable)assetDescription key:(NSString * _Nullable)key url:(NSString * _Nullable)url button:(ActitoAssetButton * _Nullable)button metaData:(ActitoAssetMetaData * _Nullable)metaData extra:(NSDictionary<NSString *,id> * _Nonnull)extra __attribute__((objc_designated_initializer));
		[Export ("initWithTitle:assetDescription:key:url:button:metaData:extra:")]
		[DesignatedInitializer]
		NativeHandle Constructor (string title, [NullAllowed] string assetDescription, [NullAllowed] string key, [NullAllowed] string url, [NullAllowed] ActitoAssetButton button, [NullAllowed] ActitoAssetMetaData metaData, NSDictionary<NSString, NSObject> extra);
	}

	// @interface ActitoAssetButton : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC19ActitoAssetsBinding17ActitoAssetButton")]
	[DisableDefaultCtor]
	interface ActitoAssetButton
	{
		// @property (readonly, copy, nonatomic) NSString * _Nullable label;
		[NullAllowed, Export ("label")]
		string Label { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable action;
		[NullAllowed, Export ("action")]
		string Action { get; }

		// -(instancetype _Nonnull)initWithLabel:(NSString * _Nullable)label action:(NSString * _Nullable)action __attribute__((objc_designated_initializer));
		[Export ("initWithLabel:action:")]
		[DesignatedInitializer]
		NativeHandle Constructor ([NullAllowed] string label, [NullAllowed] string action);
	}

	// @interface ActitoAssetMetaData : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC19ActitoAssetsBinding19ActitoAssetMetaData")]
	[DisableDefaultCtor]
	interface ActitoAssetMetaData
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull originalFileName;
		[Export ("originalFileName")]
		string OriginalFileName { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull contentType;
		[Export ("contentType")]
		string ContentType { get; }

		// @property (readonly, nonatomic) NSInteger contentLength;
		[Export ("contentLength")]
		nint ContentLength { get; }

		// -(instancetype _Nonnull)initWithOriginalFileName:(NSString * _Nonnull)originalFileName contentType:(NSString * _Nonnull)contentType contentLength:(NSInteger)contentLength __attribute__((objc_designated_initializer));
		[Export ("initWithOriginalFileName:contentType:contentLength:")]
		[DesignatedInitializer]
		NativeHandle Constructor (string originalFileName, string contentType, nint contentLength);
	}

	// @interface ActitoAssetsNativeBinding : NSObject
	[BaseType (typeof(NSObject))]
	interface ActitoAssetsNativeBinding
	{
		// -(void)fetch:(NSString * _Nonnull)group :(void (^ _Nonnull)(NSArray<ActitoAsset *> * _Nonnull))onSuccess :(void (^ _Nonnull)(NSError * _Nonnull))onFailure;
		[Export ("fetch:::")]
		void Fetch (string group, Action<NSArray<ActitoAsset>> onSuccess, Action<NSError> onFailure);
	}
}
