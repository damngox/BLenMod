using System;
using System.Collections.Generic;

namespace BlockEnhancementMod;

public class English : ILanguage
{
	public string ModSettings { get; } = "Enhancement Mod";

	public string UnifiedFriction { get; } = " Unified Friction";

	public string Enhancement { get; } = "Enhancement";

	public string AdditionalFunction { get; } = " Enhance More";

	public string BuildSurface { get; } = " Show BuildSurface's Collision and Mass Slider";

	public string DisplayWarning { get; } = " Rocket Warning in First Person Camera";

	public string ShowRadar { get; } = " Display Radar";

	public string AsRadar { get; } = "As Radar";

	public string MarkTarget { get; } = " Display Rocket Target & Projectile Interception Point";

	public string DisplayRocketCount { get; } = " Display Rocket Count";

	public string RemainingRockets { get; } = " Rocket Count";

	public string TrackTarget { get; } = "Search Target";

	public string GroupedFire { get; } = "Grouped Launch";

	public string GroupFireRate { get; } = "Goruped Launch Rate";

	public string AutoRelease { get; } = "Auto Eject";

	public string SearchMode { get; } = "Search Mode";

	public string DefaultAuto { get; } = "Default " + Environment.NewLine + "Auto Search";

	public string DefaultManual { get; } = "Default " + Environment.NewLine + " Manual Search";

	public string DefaultPassive { get; } = "Receive Target" + Environment.NewLine + "From Detector";

	public List<string> RadarType { get; } = new List<string> { "Active Radar", "Passive Radar" };

	public List<string> SettingType { get; } = new List<string> { "Rocket Setting", "Radar Setting" };

	public string ZoomControlMode { get; } = "Zoom Contorol";

	public string MouseWheelZoomControl { get; } = "Zoom" + Environment.NewLine + "Mouse Wheel";

	public string KeyboardZoomControl { get; } = "Zoom" + Environment.NewLine + "Keyboard";

	public string Prediction { get; } = "Prediction";

	public string ProjectileSpeed { get; } = "Projectile Speed";

	public string ShowProjectileInterception { get; } = "Show Projectile" + Environment.NewLine + "Interception Point";

	public string ImpactFuze { get; } = "Impact Fuze";

	public string ProximityFuze { get; } = "Proximity Fuze";

	public string NoSmoke { get; } = "No Smoke";

	public string HighExplo { get; } = "High-Explosive";

	public string SearchAngle { get; } = "Search Angle";

	public string CloseRange { get; } = "Proximity" + Environment.NewLine + "Range";

	public string TorqueOnRocket { get; } = "Turning" + Environment.NewLine + "Torque";

	public string RocketStability { get; } = "Aerodynamics" + Environment.NewLine + "After Launch";

	public string GuideDelay { get; } = "Guide Delay";

	public string LockTarget { get; } = "Lock Target";

	public string ManualOverride { get; } = "Manual Override" + Environment.NewLine + "Target Switch";

	public string RecordTarget { get; } = "Save Target";

	public string FirstPersonSmooth { get; } = "FP Smooth";

	public string ZoomIn { get; } = "Zoom In";

	public string ZoomOut { get; } = "Zoom Out";

	public string ZoomSpeed { get; } = "Zoom Speed";

	public string PauseTracking { get; } = "Pause/Resume" + Environment.NewLine + "Tracking";

	public string SinglePlayerTeam { get; } = "Single Player" + Environment.NewLine + "Team";

	public string CvJoint { get; } = "Universal Joint";

	public string FireInterval { get; } = "Fire Interval";

	public string RandomDelay { get; } = "Random Delay";

	public string Recoil { get; } = "Recoil";

	public string CustomBullet { get; } = "Custom Cannonball";

	public string InheritSize { get; } = "Inherit Size";

	public string BulletMass { get; } = "Cannonball Mass";

	public string BulletDrag { get; } = "Cannonball Drag";

	public string BulletDelayCollision { get; } = "Delay Collision";

	public string Trail { get; } = "Trail";

	public string TrailLength { get; } = "Trail Length";

	public string TrailColor { get; } = "Trail Color";

	public string ExplodeForce { get; } = "Exploding" + Environment.NewLine + "Force";

	public string ExplodeTorque { get; } = "Exploding" + Environment.NewLine + "Torque";

	public List<string> MetalHardness { get; } = new List<string> { "Low Carbon Steel", "Mid Carbon Steel", "High Carbon Steel" };

	public List<string> WoodenHardness { get; } = new List<string> { "Soft Wood", "Median-Soft Wood", "Hard Wood", "Very Hard Wood" };

	public string Friction { get; } = "Friction";

	public string Bounciness { get; } = "Bounciness";

	public string Collision { get; } = "Collision";

	public string Damper { get; } = "Damper";

	public string Limit { get; } = "Limit";

	public string Extend { get; } = "Extend";

	public string Retract { get; } = "Retract";

	public string HydraulicMode { get; } = "Hydraulic Mode ";

	public string FeedSpeed { get; } = "Feed Speed";

	public string ExtendLimit { get; } = "Extension" + Environment.NewLine + "Limit";

	public string RetractLimit { get; } = "Retraction" + Environment.NewLine + "Limit";

	public string RotatingSpeed { get; } = "Rotating" + Environment.NewLine + "Speed";

	public string CustomCollider { get; } = "Custom Collider";

	public string ShowCollider { get; } = "Show Collider";

	public string Drag { get; } = "Drag";

	public string ReturnToCenter { get; } = "ReturnToCenter";

	public string Near { get; } = "Near";

	public string ThrustForce { get; } = "Thrust Force";

	public string FlameColor { get; } = "Flame Color";

	public string Boiling { get; } = "Boiling";

	public string Enabled { get; } = "Switch";

	public string EnabledOnAwake { get; } = "Enabled On Awake";

	public string ToggleMode { get; } = "Toggle Mode";

	public string LiftIndicator { get; } = "Lift Indicator";

	public string ChangeSpeed { get; } = "Change Speed";

	public string AddSpeed { get; } = "Add Speed";

	public string ReduceSpeed { get; } = "Reduce Speed";

	public string ChangeChannel { get; } = "Change Channel";

	public string WidthPixel { get; } = "Width Pixel";

	public string HeightPixel { get; } = "Height Pixel";

	public string Switch { get; } = "Switch";

	public List<string> NullChannelList { get; } = new List<string> { "No Channel" };

	public string Play { get; } = "Play";

	public string Stop { get; } = "Stop";

	public string Mute { get; } = "Mute";

	public string Next { get; } = "Next";

	public string Last { get; } = "Last";

	public string Loop { get; } = "Loop";

	public string OneShot { get; } = "OneShot";

	public string ReleaseToPause { get; } = "Release" + Environment.NewLine + "To Pause";

	public string ReleaseToStop { get; } = "Release" + Environment.NewLine + "To Stop";

	public string OnCollision { get; } = "On Collision";

	public string Volume { get; } = "Volume";

	public string Pitch { get; } = "Pitch";

	public string Distance { get; } = "Distance";

	public string Doppler { get; } = "Doppler";

	public string SpatialBlend { get; } = "Spatial Blend";

	public string Effected { get; } = "Enable Switch";

	public string DragTogether { get; } = "Drag Together";
}
