using System.Collections.Generic;

namespace BlockEnhancementMod;

public interface ILanguage
{
	string ModSettings { get; }

	string UnifiedFriction { get; }

	string Enhancement { get; }

	string AdditionalFunction { get; }

	string BuildSurface { get; }

	string DisplayWarning { get; }

	string MarkTarget { get; }

	string DisplayRocketCount { get; }

	string RemainingRockets { get; }

	string TrackTarget { get; }

	string GroupedFire { get; }

	string GroupFireRate { get; }

	string AutoRelease { get; }

	string AsRadar { get; }

	string SearchMode { get; }

	string DefaultAuto { get; }

	string DefaultManual { get; }

	string DefaultPassive { get; }

	List<string> RadarType { get; }

	List<string> SettingType { get; }

	string ZoomControlMode { get; }

	string MouseWheelZoomControl { get; }

	string KeyboardZoomControl { get; }

	string Prediction { get; }

	string ProjectileSpeed { get; }

	string ShowProjectileInterception { get; }

	string ImpactFuze { get; }

	string ProximityFuze { get; }

	string NoSmoke { get; }

	string HighExplo { get; }

	string SearchAngle { get; }

	string CloseRange { get; }

	string ShowRadar { get; }

	string TorqueOnRocket { get; }

	string RocketStability { get; }

	string GuideDelay { get; }

	string LockTarget { get; }

	string ManualOverride { get; }

	string RecordTarget { get; }

	string FirstPersonSmooth { get; }

	string ZoomIn { get; }

	string ZoomOut { get; }

	string ZoomSpeed { get; }

	string PauseTracking { get; }

	string SinglePlayerTeam { get; }

	string CvJoint { get; }

	string FireInterval { get; }

	string RandomDelay { get; }

	string Recoil { get; }

	string CustomBullet { get; }

	string InheritSize { get; }

	string BulletMass { get; }

	string BulletDrag { get; }

	string BulletDelayCollision { get; }

	string Trail { get; }

	string TrailLength { get; }

	string TrailColor { get; }

	string ExplodeForce { get; }

	string ExplodeTorque { get; }

	List<string> MetalHardness { get; }

	List<string> WoodenHardness { get; }

	string Friction { get; }

	string Bounciness { get; }

	string Collision { get; }

	string Damper { get; }

	string Limit { get; }

	string Extend { get; }

	string Retract { get; }

	string HydraulicMode { get; }

	string FeedSpeed { get; }

	string ExtendLimit { get; }

	string RetractLimit { get; }

	string RotatingSpeed { get; }

	string CustomCollider { get; }

	string ShowCollider { get; }

	string Drag { get; }

	string ReturnToCenter { get; }

	string Near { get; }

	string ThrustForce { get; }

	string FlameColor { get; }

	string Boiling { get; }

	string Enabled { get; }

	string EnabledOnAwake { get; }

	string ToggleMode { get; }

	string LiftIndicator { get; }

	string ChangeSpeed { get; }

	string AddSpeed { get; }

	string ReduceSpeed { get; }

	string ChangeChannel { get; }

	string WidthPixel { get; }

	string HeightPixel { get; }

	string Switch { get; }

	List<string> NullChannelList { get; }

	string Play { get; }

	string Stop { get; }

	string Mute { get; }

	string Next { get; }

	string Last { get; }

	string Loop { get; }

	string OneShot { get; }

	string ReleaseToPause { get; }

	string ReleaseToStop { get; }

	string OnCollision { get; }

	string Volume { get; }

	string Pitch { get; }

	string Distance { get; }

	string Doppler { get; }

	string SpatialBlend { get; }

	string Effected { get; }

	string DragTogether { get; }
}
