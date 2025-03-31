using System;
using System.Collections.Generic;

namespace BlockEnhancementMod;

public class Chinese : ILanguage
{
	public string ModSettings { get; } = "扩展模组设置";

	public string UnifiedFriction { get; } = " 统一摩擦";

	public string Enhancement { get; } = "进阶属性";

	public string AdditionalFunction { get; } = " 增强性能";

	public string BuildSurface { get; } = " 显示蒙皮块的碰撞和质量滑条";

	public string DisplayWarning { get; } = " 第一人称下显示火箭警告";

	public string ShowRadar { get; } = " 显示雷达";

	public string AsRadar { get; } = "作为雷达";

	public string MarkTarget { get; } = " 标记火箭目标及着弹点";

	public string DisplayRocketCount { get; } = " 显示剩余火箭量";

	public string RemainingRockets { get; } = " 残余火箭";

	public string TrackTarget { get; } = "搜索目标";

	public string GroupedFire { get; } = "同组依次发射";

	public string GroupFireRate { get; } = "同组发射间隔";

	public string AutoRelease { get; } = "自动分离";

	public string SearchMode { get; } = "搜索模式";

	public string DefaultAuto { get; } = "默认自动搜索";

	public string DefaultManual { get; } = "默认手动搜索";

	public string DefaultPassive { get; } = "被动接受目标";

	public List<string> RadarType { get; } = new List<string> { "主动雷达", "被动雷达" };

	public List<string> SettingType { get; } = new List<string> { "火箭设置", "雷达设置" };

	public string ZoomControlMode { get; } = "变焦控制";

	public string MouseWheelZoomControl { get; } = "鼠标滚轮变焦";

	public string KeyboardZoomControl { get; } = "按键变焦";

	public string Prediction { get; } = "预测";

	public string ProjectileSpeed { get; } = "炮弹速度";

	public string ShowProjectileInterception { get; } = "显示炮弹落点";

	public string ImpactFuze { get; } = "碰炸";

	public string ProximityFuze { get; } = "近炸";

	public string NoSmoke { get; } = "无烟";

	public string HighExplo { get; } = "高爆";

	public string SearchAngle { get; } = "搜索角度";

	public string CloseRange { get; } = "近炸距离";

	public string TorqueOnRocket { get; } = "扭转力度";

	public string RocketStability { get; } = "发射后气动";

	public string GuideDelay { get; } = "追踪延迟";

	public string LockTarget { get; } = "锁定目标";

	public string ManualOverride { get; } = "手动覆盖" + Environment.NewLine + "目标开关";

	public string RecordTarget { get; } = "记录目标";

	public string FirstPersonSmooth { get; } = "第一人称" + Environment.NewLine + "平滑";

	public string ZoomIn { get; } = "增加焦距";

	public string ZoomOut { get; } = "减小焦距";

	public string ZoomSpeed { get; } = "变焦速度";

	public string PauseTracking { get; } = "暂停/恢复追踪";

	public string SinglePlayerTeam { get; } = "单人模式队伍";

	public string CvJoint { get; } = "万向节";

	public string FireInterval { get; } = "发射间隔";

	public string RandomDelay { get; } = "随机延迟";

	public string Recoil { get; } = "后坐力";

	public string CustomBullet { get; } = "自定子弹";

	public string InheritSize { get; } = "继承尺寸";

	public string BulletMass { get; } = "炮弹质量";

	public string BulletDrag { get; } = "炮弹阻力";

	public string BulletDelayCollision { get; } = "碰撞延时";

	public string Trail { get; } = "显示尾迹";

	public string TrailLength { get; } = "尾迹长度";

	public string TrailColor { get; } = "尾迹颜色";

	public string ExplodeForce { get; } = "爆炸力";

	public string ExplodeTorque { get; } = "爆炸扭矩";

	public List<string> MetalHardness { get; } = new List<string> { "低碳钢", "中碳钢", "高碳钢" };

	public List<string> WoodenHardness { get; } = new List<string> { "朽木", "桦木", "梨木", "檀木" };

	public string Friction { get; } = "摩擦力";

	public string Bounciness { get; } = "弹力";

	public string Collision { get; } = "碰撞";

	public string Damper { get; } = "阻尼";

	public string Limit { get; } = "限制";

	public string Extend { get; } = "伸出";

	public string Retract { get; } = "收缩";

	public string HydraulicMode { get; } = "液压模式";

	public string FeedSpeed { get; } = "进给速度";

	public string ExtendLimit { get; } = "伸出限制";

	public string RetractLimit { get; } = "收缩限制";

	public string RotatingSpeed { get; } = "旋转速度";

	public string CustomCollider { get; } = "自定碰撞";

	public string ShowCollider { get; } = "显示碰撞";

	public string Drag { get; } = "阻力";

	public string ReturnToCenter { get; } = "自动回正";

	public string Near { get; } = "就近";

	public string ThrustForce { get; } = "推力";

	public string FlameColor { get; } = "火焰颜色";

	public string Boiling { get; } = "沸腾";

	public string Enabled { get; } = "气动开关";

	public string EnabledOnAwake { get; } = "初始生效";

	public string ToggleMode { get; } = "持续生效模式";

	public string LiftIndicator { get; } = "升力指示";

	public string ChangeSpeed { get; } = "改变速度";

	public string AddSpeed { get; } = "加速";

	public string ReduceSpeed { get; } = "减速";

	public string ChangeChannel { get; } = "更换频道";

	public string WidthPixel { get; } = "宽度像素";

	public string HeightPixel { get; } = "高度像素";

	public string Switch { get; } = "开关";

	public List<string> NullChannelList { get; } = new List<string> { "无信号" };

	public string Play { get; } = "播放";

	public string Stop { get; } = "停止";

	public string Mute { get; } = "静音";

	public string Next { get; } = "下一个";

	public string Last { get; } = "上一个";

	public string Loop { get; } = "循环";

	public string OneShot { get; } = "单次播放";

	public string ReleaseToPause { get; } = "放开暂停";

	public string ReleaseToStop { get; } = "放开停止";

	public string OnCollision { get; } = "碰撞时";

	public string Volume { get; } = "音量";

	public string Pitch { get; } = "音调";

	public string Distance { get; } = "传播距离";

	public string Doppler { get; } = "多普勒效应";

	public string SpatialBlend { get; } = "空间衰减";

	public string Effected { get; } = "使能开关";

	public string DragTogether { get; } = "同时关闭阻力";
}
