using System;
using CoreLocation;
using Foundation;
using ObjCRuntime;

namespace ActitoSdk.Geo.iOS.Binding
{
	// @interface ActitoBeacon : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC16ActitoGeoBinding12ActitoBeacon")]
	[DisableDefaultCtor]
	interface ActitoBeacon
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull beaconId;
		[Export ("beaconId")]
		string BeaconId { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull name;
		[Export ("name")]
		string Name { get; }

		// @property (readonly, nonatomic) NSInteger major;
		[Export ("major")]
		nint Major { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nullable minor;
		[NullAllowed, Export ("minor", ArgumentSemantic.Strong)]
		NSNumber Minor { get; }

		// @property (readonly, nonatomic) BOOL triggers;
		[Export ("triggers")]
		bool Triggers { get; }

		// @property (readonly, nonatomic) enum ActitoBeaconProximity proximity;
		[Export ("proximity")]
		ActitoBeaconProximity Proximity { get; }

		// -(instancetype _Nonnull)initWithBeaconId:(NSString * _Nonnull)beaconId name:(NSString * _Nonnull)name major:(NSInteger)major minor:(NSNumber * _Nullable)minor triggers:(BOOL)triggers proximity:(enum ActitoBeaconProximity)proximity __attribute__((objc_designated_initializer));
		[Export ("initWithBeaconId:name:major:minor:triggers:proximity:")]
		[DesignatedInitializer]
		NativeHandle Constructor (string beaconId, string name, nint major, [NullAllowed] NSNumber minor, bool triggers, ActitoBeaconProximity proximity);
	}

	// @interface ActitoGeoNativeBinding : NSObject
	[BaseType (typeof(NSObject))]
	interface ActitoGeoNativeBinding
	{
		[Wrap ("WeakDelegate")]
		[NullAllowed]
		ActitoGeoNativeBindingDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<ActitoGeoNativeBindingDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// @property (readonly, nonatomic) BOOL hasLocationServicesEnabled;
		[Export ("hasLocationServicesEnabled")]
		bool HasLocationServicesEnabled { get; }

		// @property (readonly, nonatomic) BOOL hasBluetoothEnabled;
		[Export ("hasBluetoothEnabled")]
		bool HasBluetoothEnabled { get; }

		// @property (readonly, copy, nonatomic) NSArray<ActitoRegion *> * _Nonnull monitoredRegions;
		[Export ("monitoredRegions", ArgumentSemantic.Copy)]
		ActitoRegion[] MonitoredRegions { get; }

		// @property (readonly, copy, nonatomic) NSArray<ActitoRegion *> * _Nonnull enteredRegions;
		[Export ("enteredRegions", ArgumentSemantic.Copy)]
		ActitoRegion[] EnteredRegions { get; }

		// -(void)enableLocationUpdates;
		[Export ("enableLocationUpdates")]
		void EnableLocationUpdates ();

		// -(void)disableLocationUpdates;
		[Export ("disableLocationUpdates")]
		void DisableLocationUpdates ();
	}

	// @protocol ActitoGeoNativeBindingDelegate <NSObject>
	[Protocol (Name = "_TtP16ActitoGeoBinding30ActitoGeoNativeBindingDelegate_"), Model]
	[BaseType (typeof(NSObject), Name = "_TtP16ActitoGeoBinding30ActitoGeoNativeBindingDelegate_")]
	interface ActitoGeoNativeBindingDelegate
	{
		// @required -(void)actito:(ActitoGeoNativeBinding * _Nonnull)actitoGeo didUpdateLocations:(NSArray<ActitoLocation *> * _Nonnull)locations;
		[Abstract]
		[Export ("actito:didUpdateLocations:")]
		void DidUpdateLocations (ActitoGeoNativeBinding actitoGeo, ActitoLocation[] locations);

		// @required -(void)actito:(ActitoGeoNativeBinding * _Nonnull)actitoGeo didFailWith:(NSError * _Nonnull)error;
		[Abstract]
		[Export ("actito:didFailWith:")]
		void DidFailWith (ActitoGeoNativeBinding actitoGeo, NSError error);

		// @required -(void)actito:(ActitoGeoNativeBinding * _Nonnull)actitoGeo didStartMonitoringForRegion:(ActitoRegion * _Nonnull)region;
		[Abstract]
		[Export ("actito:didStartMonitoringForRegion:")]
		void DidStartMonitoringForRegion (ActitoGeoNativeBinding actitoGeo, ActitoRegion region);

		// @required -(void)actito:(ActitoGeoNativeBinding * _Nonnull)actitoGeo didStartMonitoringForBeacon:(ActitoBeacon * _Nonnull)beacon;
		[Abstract]
		[Export ("actito:didStartMonitoringForBeacon:")]
		void DidStartMonitoringForBeacon (ActitoGeoNativeBinding actitoGeo, ActitoBeacon beacon);

		// @required -(void)actito:(ActitoGeoNativeBinding * _Nonnull)actitoGeo monitoringDidFailForRegion:(ActitoRegion * _Nonnull)region with:(NSError * _Nonnull)error;
		[Abstract]
		[Export ("actito:monitoringDidFailForRegion:with:")]
		void MonitoringDidFailForRegion (ActitoGeoNativeBinding actitoGeo, ActitoRegion region, NSError error);

		// @required -(void)actito:(ActitoGeoNativeBinding * _Nonnull)actitoGeo monitoringDidFailForBeacon:(ActitoBeacon * _Nonnull)beacon with:(NSError * _Nonnull)error;
		[Abstract]
		[Export ("actito:monitoringDidFailForBeacon:with:")]
		void MonitoringDidFailForBeacon (ActitoGeoNativeBinding actitoGeo, ActitoBeacon beacon, NSError error);

		// @required -(void)actito:(ActitoGeoNativeBinding * _Nonnull)actitoGeo didDetermineState:(CLRegionState)state forRegion:(ActitoRegion * _Nonnull)region;
		[Abstract]
		[Export ("actito:didDetermineState:forRegion:")]
		void DidDetermineState (ActitoGeoNativeBinding actitoGeo, CLRegionState state, ActitoRegion region);

		// @required -(void)actito:(ActitoGeoNativeBinding * _Nonnull)actitoGeo didDetermineState:(CLRegionState)state forBeacon:(ActitoBeacon * _Nonnull)beacon;
		[Abstract]
		[Export ("actito:didDetermineState:forBeacon:")]
		void DidDetermineState (ActitoGeoNativeBinding actitoGeo, CLRegionState state, ActitoBeacon beacon);

		// @required -(void)actito:(ActitoGeoNativeBinding * _Nonnull)actitoGeo didEnterRegion:(ActitoRegion * _Nonnull)region;
		[Abstract]
		[Export ("actito:didEnterRegion:")]
		void DidEnterRegion (ActitoGeoNativeBinding actitoGeo, ActitoRegion region);

		// @required -(void)actito:(ActitoGeoNativeBinding * _Nonnull)actitoGeo didEnterBeacon:(ActitoBeacon * _Nonnull)beacon;
		[Abstract]
		[Export ("actito:didEnterBeacon:")]
		void DidEnterBeacon (ActitoGeoNativeBinding actitoGeo, ActitoBeacon beacon);

		// @required -(void)actito:(ActitoGeoNativeBinding * _Nonnull)actitoGeo didExitRegion:(ActitoRegion * _Nonnull)region;
		[Abstract]
		[Export ("actito:didExitRegion:")]
		void DidExitRegion (ActitoGeoNativeBinding actitoGeo, ActitoRegion region);

		// @required -(void)actito:(ActitoGeoNativeBinding * _Nonnull)actitoGeo didExitBeacon:(ActitoBeacon * _Nonnull)beacon;
		[Abstract]
		[Export ("actito:didExitBeacon:")]
		void DidExitBeacon (ActitoGeoNativeBinding actitoGeo, ActitoBeacon beacon);

		// @required -(void)actito:(ActitoGeoNativeBinding * _Nonnull)actitoGeo didVisit:(ActitoVisit * _Nonnull)visit;
		[Abstract]
		[Export ("actito:didVisit:")]
		void DidVisit (ActitoGeoNativeBinding actitoGeo, ActitoVisit visit);

		// @required -(void)actito:(ActitoGeoNativeBinding * _Nonnull)actitoGeo didUpdateHeading:(ActitoHeading * _Nonnull)heading;
		[Abstract]
		[Export ("actito:didUpdateHeading:")]
		void DidUpdateHeading (ActitoGeoNativeBinding actitoGeo, ActitoHeading heading);

		// @required -(void)actito:(ActitoGeoNativeBinding * _Nonnull)actitoGeo didRange:(NSArray<ActitoBeacon *> * _Nonnull)beacons in:(ActitoRegion * _Nonnull)region;
		[Abstract]
		[Export ("actito:didRange:in:")]
		void DidRange (ActitoGeoNativeBinding actitoGeo, ActitoBeacon[] beacons, ActitoRegion region);

		// @required -(void)actito:(ActitoGeoNativeBinding * _Nonnull)actitoGeo didFailRangingFor:(ActitoRegion * _Nonnull)region with:(NSError * _Nonnull)error;
		[Abstract]
		[Export ("actito:didFailRangingFor:with:")]
		void DidFailRangingFor (ActitoGeoNativeBinding actitoGeo, ActitoRegion region, NSError error);
	}

	// @interface ActitoHeading : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC16ActitoGeoBinding13ActitoHeading")]
	[DisableDefaultCtor]
	interface ActitoHeading
	{
		// @property (readonly, nonatomic) double magneticHeading;
		[Export ("magneticHeading")]
		double MagneticHeading { get; }

		// @property (readonly, nonatomic) double trueHeading;
		[Export ("trueHeading")]
		double TrueHeading { get; }

		// @property (readonly, nonatomic) double headingAccuracy;
		[Export ("headingAccuracy")]
		double HeadingAccuracy { get; }

		// @property (readonly, nonatomic) double x;
		[Export ("x")]
		double X { get; }

		// @property (readonly, nonatomic) double y;
		[Export ("y")]
		double Y { get; }

		// @property (readonly, nonatomic) double z;
		[Export ("z")]
		double Z { get; }

		// @property (readonly, copy, nonatomic) NSDate * _Nonnull timestamp;
		[Export ("timestamp", ArgumentSemantic.Copy)]
		NSDate Timestamp { get; }

		// -(instancetype _Nonnull)initWithMagneticHeading:(double)magneticHeading trueHeading:(double)trueHeading headingAccuracy:(double)headingAccuracy x:(double)x y:(double)y z:(double)z timestamp:(NSDate * _Nonnull)timestamp __attribute__((objc_designated_initializer));
		[Export ("initWithMagneticHeading:trueHeading:headingAccuracy:x:y:z:timestamp:")]
		[DesignatedInitializer]
		NativeHandle Constructor (double magneticHeading, double trueHeading, double headingAccuracy, double x, double y, double z, NSDate timestamp);
	}

	// @interface ActitoLocation : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC16ActitoGeoBinding14ActitoLocation")]
	[DisableDefaultCtor]
	interface ActitoLocation
	{
		// @property (readonly, nonatomic) double latitude;
		[Export ("latitude")]
		double Latitude { get; }

		// @property (readonly, nonatomic) double longitude;
		[Export ("longitude")]
		double Longitude { get; }

		// @property (readonly, nonatomic) double altitude;
		[Export ("altitude")]
		double Altitude { get; }

		// @property (readonly, nonatomic) double course;
		[Export ("course")]
		double Course { get; }

		// @property (readonly, nonatomic) double speed;
		[Export ("speed")]
		double Speed { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nullable floor;
		[NullAllowed, Export ("floor", ArgumentSemantic.Strong)]
		NSNumber Floor { get; }

		// @property (readonly, nonatomic) double horizontalAccuracy;
		[Export ("horizontalAccuracy")]
		double HorizontalAccuracy { get; }

		// @property (readonly, nonatomic) double verticalAccuracy;
		[Export ("verticalAccuracy")]
		double VerticalAccuracy { get; }

		// @property (readonly, copy, nonatomic) NSDate * _Nonnull timestamp;
		[Export ("timestamp", ArgumentSemantic.Copy)]
		NSDate Timestamp { get; }

		// -(instancetype _Nonnull)initWithLatitude:(double)latitude longitude:(double)longitude altitude:(double)altitude course:(double)course speed:(double)speed floor:(NSNumber * _Nullable)floor horizontalAccuracy:(double)horizontalAccuracy verticalAccuracy:(double)verticalAccuracy timestamp:(NSDate * _Nonnull)timestamp __attribute__((objc_designated_initializer));
		[Export ("initWithLatitude:longitude:altitude:course:speed:floor:horizontalAccuracy:verticalAccuracy:timestamp:")]
		[DesignatedInitializer]
		NativeHandle Constructor (double latitude, double longitude, double altitude, double course, double speed, [NullAllowed] NSNumber floor, double horizontalAccuracy, double verticalAccuracy, NSDate timestamp);
	}

	// @interface ActitoRegion : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC16ActitoGeoBinding12ActitoRegion")]
	[DisableDefaultCtor]
	interface ActitoRegion
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull regionId;
		[Export ("regionId")]
		string RegionId { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull name;
		[Export ("name")]
		string Name { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable regionDescription;
		[NullAllowed, Export ("regionDescription")]
		string RegionDescription { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable referenceKey;
		[NullAllowed, Export ("referenceKey")]
		string ReferenceKey { get; }

		// @property (readonly, nonatomic, strong) ActitoRegionGeometry * _Nonnull geometry;
		[Export ("geometry", ArgumentSemantic.Strong)]
		ActitoRegionGeometry Geometry { get; }

		// @property (readonly, nonatomic, strong) ActitoRegionAdvancedGeometry * _Nullable advancedGeometry;
		[NullAllowed, Export ("advancedGeometry", ArgumentSemantic.Strong)]
		ActitoRegionAdvancedGeometry AdvancedGeometry { get; }

		// @property (readonly, nonatomic, strong) NSNumber * _Nullable major;
		[NullAllowed, Export ("major", ArgumentSemantic.Strong)]
		NSNumber Major { get; }

		// @property (readonly, nonatomic) double distance;
		[Export ("distance")]
		double Distance { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull timeZone;
		[Export ("timeZone")]
		string TimeZone { get; }

		// @property (readonly, nonatomic) double timeZoneOffset;
		[Export ("timeZoneOffset")]
		double TimeZoneOffset { get; }

		// -(instancetype _Nonnull)initWithRegionId:(NSString * _Nonnull)regionId name:(NSString * _Nonnull)name regionDescription:(NSString * _Nullable)regionDescription referenceKey:(NSString * _Nullable)referenceKey geometry:(ActitoRegionGeometry * _Nonnull)geometry advancedGeometry:(ActitoRegionAdvancedGeometry * _Nullable)advancedGeometry major:(NSNumber * _Nullable)major distance:(double)distance timeZone:(NSString * _Nonnull)timeZone timeZoneOffset:(double)timeZoneOffset __attribute__((objc_designated_initializer));
		[Export ("initWithRegionId:name:regionDescription:referenceKey:geometry:advancedGeometry:major:distance:timeZone:timeZoneOffset:")]
		[DesignatedInitializer]
		NativeHandle Constructor (string regionId, string name, [NullAllowed] string regionDescription, [NullAllowed] string referenceKey, ActitoRegionGeometry geometry, [NullAllowed] ActitoRegionAdvancedGeometry advancedGeometry, [NullAllowed] NSNumber major, double distance, string timeZone, double timeZoneOffset);
	}

	// @interface ActitoRegionAdvancedGeometry : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC16ActitoGeoBinding28ActitoRegionAdvancedGeometry")]
	[DisableDefaultCtor]
	interface ActitoRegionAdvancedGeometry
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull type;
		[Export ("type")]
		string Type { get; }

		// @property (readonly, copy, nonatomic) NSArray<ActitoRegionCoordinate *> * _Nonnull coordinates;
		[Export ("coordinates", ArgumentSemantic.Copy)]
		ActitoRegionCoordinate[] Coordinates { get; }

		// -(instancetype _Nonnull)initWithType:(NSString * _Nonnull)type coordinates:(NSArray<ActitoRegionCoordinate *> * _Nonnull)coordinates __attribute__((objc_designated_initializer));
		[Export ("initWithType:coordinates:")]
		[DesignatedInitializer]
		NativeHandle Constructor (string type, ActitoRegionCoordinate[] coordinates);
	}

	// @interface ActitoRegionCoordinate : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC16ActitoGeoBinding22ActitoRegionCoordinate")]
	[DisableDefaultCtor]
	interface ActitoRegionCoordinate
	{
		// @property (readonly, nonatomic) double latitude;
		[Export ("latitude")]
		double Latitude { get; }

		// @property (readonly, nonatomic) double longitude;
		[Export ("longitude")]
		double Longitude { get; }

		// -(instancetype _Nonnull)initWithLatitude:(double)latitude longitude:(double)longitude __attribute__((objc_designated_initializer));
		[Export ("initWithLatitude:longitude:")]
		[DesignatedInitializer]
		NativeHandle Constructor (double latitude, double longitude);
	}

	// @interface ActitoRegionGeometry : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC16ActitoGeoBinding20ActitoRegionGeometry")]
	[DisableDefaultCtor]
	interface ActitoRegionGeometry
	{
		// @property (readonly, copy, nonatomic) NSString * _Nonnull type;
		[Export ("type")]
		string Type { get; }

		// @property (readonly, nonatomic, strong) ActitoRegionCoordinate * _Nonnull coordinate;
		[Export ("coordinate", ArgumentSemantic.Strong)]
		ActitoRegionCoordinate Coordinate { get; }

		// -(instancetype _Nonnull)initWithType:(NSString * _Nonnull)type coordinate:(ActitoRegionCoordinate * _Nonnull)coordinate __attribute__((objc_designated_initializer));
		[Export ("initWithType:coordinate:")]
		[DesignatedInitializer]
		NativeHandle Constructor (string type, ActitoRegionCoordinate coordinate);
	}

	// @interface ActitoVisit : NSObject
	[BaseType (typeof(NSObject), Name = "_TtC16ActitoGeoBinding11ActitoVisit")]
	[DisableDefaultCtor]
	interface ActitoVisit
	{
		// @property (readonly, copy, nonatomic) NSDate * _Nonnull departureDate;
		[Export ("departureDate", ArgumentSemantic.Copy)]
		NSDate DepartureDate { get; }

		// @property (readonly, copy, nonatomic) NSDate * _Nonnull arrivalDate;
		[Export ("arrivalDate", ArgumentSemantic.Copy)]
		NSDate ArrivalDate { get; }

		// @property (readonly, nonatomic) double latitude;
		[Export ("latitude")]
		double Latitude { get; }

		// @property (readonly, nonatomic) double longitude;
		[Export ("longitude")]
		double Longitude { get; }

		// -(instancetype _Nonnull)initWithDepartureDate:(NSDate * _Nonnull)departureDate arrivalDate:(NSDate * _Nonnull)arrivalDate latitude:(double)latitude longitude:(double)longitude __attribute__((objc_designated_initializer));
		[Export ("initWithDepartureDate:arrivalDate:latitude:longitude:")]
		[DesignatedInitializer]
		NativeHandle Constructor (NSDate departureDate, NSDate arrivalDate, double latitude, double longitude);
	}
}
