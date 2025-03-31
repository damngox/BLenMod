using System;
using System.Collections.Generic;

namespace BlockEnhancementMod;

public class Japanese : ILanguage
{
	public string ModSettings { get; } = "ブロック エンハンスメントMod";

	public string UnifiedFriction { get; } = " 摩擦を統一する";

	public string Enhancement { get; } = "機能拡張";

	public string AdditionalFunction { get; } = " さらに拡張する";

	public string BuildSurface { get; } = " ショーけんちくひょうめん 衝突と質量スライダ";

	public string DisplayWarning { get; } = " 一人称カメラでロケット警告表示";

	public string ShowRadar { get; } = " レーダー表示";

	public string AsRadar { get; } = "As Radar";

	public string MarkTarget { get; } = " ロケットのターゲット枠表示";

	public string DisplayRocketCount { get; } = " ロケットの残数表示";

	public string RemainingRockets { get; } = " ロケット残弾数";

	public string TrackTarget { get; } = "ロックオン";

	public string GroupedFire { get; } = "同一キーで個別発射";

	public string GroupFireRate { get; } = "発射間隔";

	public string AutoRelease { get; } = "自動解放";

	public string SearchMode { get; } = "ロックオンモード";

	public string DefaultAuto { get; } = "デフォルト " + Environment.NewLine + "自動ロックオン";

	public string DefaultManual { get; } = "デフォルト " + Environment.NewLine + " 手動ロックオン";

	public string DefaultPassive { get; } = "Receive Target" + Environment.NewLine + "From Detector";

	public List<string> RadarType { get; } = new List<string> { "能動レーダー", "受動レーダー" };

	public List<string> SettingType { get; } = new List<string> { "ロケット設定", "レーダー設定" };

	public string ZoomControlMode { get; } = "Zoom Contorol";

	public string MouseWheelZoomControl { get; } = "ズーム" + Environment.NewLine + "マウスホイール";

	public string KeyboardZoomControl { get; } = "ズーム" + Environment.NewLine + "キーボード";

	public string Prediction { get; } = "予測誘導";

	public string ProjectileSpeed { get; } = "弾速";

	public string ShowProjectileInterception { get; } = "迎撃点を示す";

	public string ImpactFuze { get; } = "衝撃起爆";

	public string ProximityFuze { get; } = "近接起爆";

	public string NoSmoke { get; } = "煙なし";

	public string HighExplo { get; } = "ボムの爆発";

	public string SearchAngle { get; } = "索敵角度";

	public string CloseRange { get; } = "起爆" + Environment.NewLine + "距離";

	public string TorqueOnRocket { get; } = "索敵時の" + Environment.NewLine + "旋回トルク";

	public string RocketStability { get; } = "発射後に" + Environment.NewLine + "空力ブロック化";

	public string GuideDelay { get; } = "誘導遅延";

	public string LockTarget { get; } = "手動ロックオン";

	public string ManualOverride { get; } = "オーバーライド" + Environment.NewLine + "目標スイッチ";

	public string RecordTarget { get; } = "ターゲット記憶";

	public string FirstPersonSmooth { get; } = "一人称時スムース";

	public string ZoomIn { get; } = "ズームイン";

	public string ZoomOut { get; } = "ズームアウト";

	public string ZoomSpeed { get; } = "ズームスピード";

	public string PauseTracking { get; } = "注視の" + Environment.NewLine + "停止/再開";

	public string SinglePlayerTeam { get; } = "Single Player" + Environment.NewLine + "Team";

	public string CvJoint { get; } = "ﾕﾆﾊﾞｰｻﾙｼﾞｮｲﾝﾄ";

	public string FireInterval { get; } = "反応速度";

	public string RandomDelay { get; } = "ランダム遅延";

	public string Recoil { get; } = "反動";

	public string CustomBullet { get; } = "砲弾のカスタム";

	public string InheritSize { get; } = "スケーリング反映";

	public string BulletMass { get; } = "砲弾の重さ";

	public string BulletDrag { get; } = "砲弾の抗力";

	public string BulletDelayCollision { get; } = "衝突の遅延";

	public string Trail { get; } = "軌跡";

	public string TrailLength { get; } = "軌跡の長さ";

	public string TrailColor { get; } = "軌跡の色";

	public string ExplodeForce { get; } = "切り離し時の" + Environment.NewLine + "初速";

	public string ExplodeTorque { get; } = "切り離し時の" + Environment.NewLine + "ひねり";

	public List<string> MetalHardness { get; } = new List<string> { "軟質", "中硬", "硬質" };

	public List<string> WoodenHardness { get; } = new List<string> { "軟質", "中硬", "硬質", "超硬質" };

	public string Friction { get; } = "摩擦";

	public string Bounciness { get; } = "弾性";

	public string Collision { get; } = "衝突";

	public string Damper { get; } = "バネの重さ";

	public string Limit { get; } = "距離";

	public string Extend { get; } = "伸長";

	public string Retract { get; } = "収縮";

	public string HydraulicMode { get; } = "油圧モード ";

	public string FeedSpeed { get; } = "油圧の強さ";

	public string ExtendLimit { get; } = "伸長" + Environment.NewLine + "距離";

	public string RetractLimit { get; } = "収縮" + Environment.NewLine + "距離";

	public string RotatingSpeed { get; } = "軸の" + Environment.NewLine + "回転速度";

	public string CustomCollider { get; } = "カスタムコライダー";

	public string ShowCollider { get; } = "コライダー表示";

	public string Drag { get; } = "抗力";

	public string ReturnToCenter { get; } = "自動で戻る";

	public string Near { get; } = "最短距離";

	public string ThrustForce { get; } = "推進力";

	public string FlameColor { get; } = "炎の色";

	public string Boiling { get; } = "常に加熱";

	public string Enabled { get; } = "有効/無効化";

	public string EnabledOnAwake { get; } = "開始時に有効化";

	public string ToggleMode { get; } = "トグルモード";

	public string LiftIndicator { get; } = "揚力方向表示";

	public string ChangeSpeed { get; } = "変化速度";

	public string AddSpeed { get; } = "速度を加える";

	public string ReduceSpeed { get; } = "速度を落とす";

	public string ChangeChannel { get; } = "チャンネル変更";

	public string WidthPixel { get; } = "幅ピクセル";

	public string HeightPixel { get; } = "高さピクセル";

	public string Switch { get; } = "スイッチ";

	public List<string> NullChannelList { get; } = new List<string> { "チャンネルなし" };

	public string Play { get; } = "遊び";

	public string Stop { get; } = "ストップ";

	public string Mute { get; } = "ミュート";

	public string Next { get; } = "次の方";

	public string Last { get; } = "前の方";

	public string Loop { get; } = "ループ";

	public string OneShot { get; } = "ワンショット";

	public string ReleaseToPause { get; } = "休止する";

	public string ReleaseToStop { get; } = "停止する";

	public string OnCollision { get; } = "衝突に関して";

	public string Volume { get; } = "体積";

	public string Pitch { get; } = "ピッチ";

	public string Distance { get; } = "ディスタンス";

	public string Doppler { get; } = "ドップラー";

	public string SpatialBlend { get; } = "空間ブレンド";

	public string Effected { get; } = "イネーブルスイッチ";

	public string DragTogether { get; } = "一緒にドラッグ";
}
