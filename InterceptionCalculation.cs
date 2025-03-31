using System;
using UnityEngine;

namespace BlockEnhancementMod;

internal class InterceptionCalculation
{
	public static int SolveBallisticArc(Vector3 projPos, float projSpeed, Vector3 targetPos, Vector3 targetVelocity, float gravity, out Vector3 s0, out float time)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		s0 = Vector3.zero;
		time = 0f;
		float x = projPos.x;
		float y = projPos.y;
		float z = projPos.z;
		float x2 = targetPos.x;
		float y2 = targetPos.y;
		float z2 = targetPos.z;
		float x3 = targetVelocity.x;
		float y3 = targetVelocity.y;
		float z3 = targetVelocity.z;
		float num = x2 - x;
		float num2 = z2 - z;
		float num3 = y2 - y;
		float num4 = -0.5f * gravity;
		float c = num4 * num4;
		float c2 = 2f * y3 * num4;
		float c3 = y3 * y3 + 2f * num3 * num4 - projSpeed * projSpeed + x3 * x3 + z3 * z3;
		float c4 = 2f * num3 * y3 + 2f * num * x3 + 2f * num2 * z3;
		float c5 = num3 * num3 + num * num + num2 * num2;
		float[] array = new float[4];
		int num5 = SolveQuartic(c, c2, c3, c4, c5, out array[0], out array[1], out array[2], out array[3]);
		Array.Sort(array);
		Vector3[] array2 = (Vector3[])(object)new Vector3[2];
		float[] array3 = new float[2];
		int num6 = 0;
		for (int i = 0; i < num5; i++)
		{
			if (num6 >= 2)
			{
				break;
			}
			float num7 = array[i];
			if (!(num7 <= 0f))
			{
				array3[num6] = num7;
				array2[num6].x = (num + x3 * num7) / num7;
				array2[num6].y = (num3 + y3 * num7 - num4 * num7 * num7) / num7;
				array2[num6].z = (num2 + z3 * num7) / num7;
				num6++;
			}
		}
		if (num6 > 0)
		{
			time = array3[0];
			s0 = array2[0];
		}
		return num6;
	}

	public static int SolveBallisticArc(Vector3 projPos, float projSpeed, Vector3 targetPos, float gravity, out Vector3 s0, out float time)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		s0 = Vector3.zero;
		time = 0f;
		Vector3 val = targetPos - projPos;
		Vector3 val2 = default(Vector3);
		((Vector3)(ref val2))._002Ector(val.x, 0f, val.z);
		float magnitude = ((Vector3)(ref val2)).magnitude;
		float num = projSpeed * projSpeed;
		float num2 = projSpeed * projSpeed * projSpeed * projSpeed;
		float y = val.y;
		float num3 = magnitude;
		float num4 = gravity * num3;
		float num5 = num2 - gravity * (gravity * num3 * num3 + 2f * y * num);
		if (num5 < 0f)
		{
			return 0;
		}
		num5 = Mathf.Sqrt(num5);
		float num6 = Mathf.Atan2(num - num5, num4);
		float num7 = Mathf.Atan2(num + num5, num4);
		int result = ((num6 == num7) ? 1 : 2);
		Vector3 normalized = ((Vector3)(ref val2)).normalized;
		s0 = normalized * Mathf.Cos(num6) * projSpeed + Vector3.up * Mathf.Sin(num6) * projSpeed;
		time = magnitude / Mathf.Cos(num6) / projSpeed;
		return result;
	}

	public static int SolveQuartic(float c0, float c1, float c2, float c3, float c4, out float s0, out float s1, out float s2, out float s3)
	{
		s0 = float.NaN;
		s1 = float.NaN;
		s2 = float.NaN;
		s3 = float.NaN;
		float[] array = new float[4];
		float num = c1 / c0;
		float num2 = c2 / c0;
		float num3 = c3 / c0;
		float num4 = c4 / c0;
		float num5 = num * num;
		float num6 = -0.375f * num5 + num2;
		float num7 = 0.125f * num5 * num - 0.5f * num * num2 + num3;
		float num8 = -0.01171875f * num5 * num5 + 0.0625f * num5 * num2 - 0.25f * num * num3 + num4;
		int num9;
		if (IsZero(num8))
		{
			array[3] = num7;
			array[2] = num6;
			array[1] = 0f;
			array[0] = 1f;
			num9 = SolveCubic(array[0], array[1], array[2], array[3], out s0, out s1, out s2);
		}
		else
		{
			array[3] = 0.5f * num8 * num6 - 0.125f * num7 * num7;
			array[2] = 0f - num8;
			array[1] = -0.5f * num6;
			array[0] = 1f;
			SolveCubic(array[0], array[1], array[2], array[3], out s0, out s1, out s2);
			float num10 = s0;
			float num11 = num10 * num10 - num8;
			float num12 = 2f * num10 - num6;
			if (IsZero(num11))
			{
				num11 = 0f;
			}
			else
			{
				if (!(num11 > 0f))
				{
					return 0;
				}
				num11 = Mathf.Sqrt(num11);
			}
			if (IsZero(num12))
			{
				num12 = 0f;
			}
			else
			{
				if (!(num12 > 0f))
				{
					return 0;
				}
				num12 = Mathf.Sqrt(num12);
			}
			array[2] = num10 - num11;
			array[1] = ((num7 < 0f) ? (0f - num12) : num12);
			array[0] = 1f;
			num9 = SolveQuadric(array[0], array[1], array[2], out s0, out s1);
			array[2] = num10 + num11;
			array[1] = ((num7 < 0f) ? num12 : (0f - num12));
			array[0] = 1f;
			if (num9 == 0)
			{
				num9 += SolveQuadric(array[0], array[1], array[2], out s0, out s1);
			}
			if (num9 == 1)
			{
				num9 += SolveQuadric(array[0], array[1], array[2], out s1, out s2);
			}
			if (num9 == 2)
			{
				num9 += SolveQuadric(array[0], array[1], array[2], out s2, out s3);
			}
		}
		float num13 = 0.25f * num;
		if (num9 > 0)
		{
			s0 -= num13;
		}
		if (num9 > 1)
		{
			s1 -= num13;
		}
		if (num9 > 2)
		{
			s2 -= num13;
		}
		if (num9 > 3)
		{
			s3 -= num13;
		}
		return num9;
	}

	public static int SolveCubic(float c0, float c1, float c2, float c3, out float s0, out float s1, out float s2)
	{
		s0 = float.NaN;
		s1 = float.NaN;
		s2 = float.NaN;
		float num = c1 / c0;
		float num2 = c2 / c0;
		float num3 = c3 / c0;
		float num4 = num * num;
		float num5 = 1f / 3f * (-1f / 3f * num4 + num2);
		float num6 = 0.5f * (2f / 27f * num * num4 - 1f / 3f * num * num2 + num3);
		float num7 = num5 * num5 * num5;
		float num8 = num6 * num6 + num7;
		int num9;
		if (IsZero(num8))
		{
			if (IsZero(num6))
			{
				s0 = 0f;
				num9 = 1;
			}
			else
			{
				float num10 = Mathf.Pow(0f - num6, 1f / 3f);
				s0 = 2f * num10;
				s1 = 0f - num10;
				num9 = 2;
			}
		}
		else if (num8 < 0f)
		{
			float num11 = 1f / 3f * Mathf.Acos((0f - num6) / Mathf.Sqrt(0f - num7));
			float num12 = 2f * Mathf.Sqrt(0f - num5);
			s0 = num12 * Mathf.Cos(num11);
			s1 = (0f - num12) * Mathf.Cos(num11 + (float)Math.PI / 3f);
			s2 = (0f - num12) * Mathf.Cos(num11 - (float)Math.PI / 3f);
			num9 = 3;
		}
		else
		{
			float num13 = Mathf.Sqrt(num8);
			float num14 = Mathf.Pow(num13 - num6, 1f / 3f);
			float num15 = 0f - Mathf.Pow(num13 + num6, 1f / 3f);
			s0 = num14 + num15;
			num9 = 1;
		}
		float num16 = 1f / 3f * num;
		if (num9 > 0)
		{
			s0 -= num16;
		}
		if (num9 > 1)
		{
			s1 -= num16;
		}
		if (num9 > 2)
		{
			s2 -= num16;
		}
		return num9;
	}

	public static int SolveQuadric(float c0, float c1, float c2, out float s0, out float s1)
	{
		s0 = float.NaN;
		s1 = float.NaN;
		float num = c1 / (2f * c0);
		float num2 = c2 / c0;
		float num3 = num * num - num2;
		if (IsZero(num3))
		{
			s0 = 0f - num;
			return 1;
		}
		if (num3 < 0f)
		{
			return 0;
		}
		float num4 = Mathf.Sqrt(num3);
		s0 = num4 - num;
		s1 = 0f - num4 - num;
		return 2;
	}

	public static bool IsZero(double d)
	{
		if (d > -0.0001)
		{
			return d < 0.0001;
		}
		return false;
	}

	public static float FirstOrderInterceptTime(float shotSpeed, Vector3 targetRelativePosition, Vector3 targetRelativeVelocity)
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		float sqrMagnitude = ((Vector3)(ref targetRelativeVelocity)).sqrMagnitude;
		if (sqrMagnitude < 0.1f)
		{
			return 0f;
		}
		float num = sqrMagnitude - shotSpeed * shotSpeed;
		if (Mathf.Abs(num) < 0.1f)
		{
			return Mathf.Max((0f - ((Vector3)(ref targetRelativePosition)).sqrMagnitude) / (2f * Vector3.Dot(targetRelativeVelocity, targetRelativePosition)), 0f);
		}
		float num2 = 2f * Vector3.Dot(targetRelativeVelocity, targetRelativePosition);
		float sqrMagnitude2 = ((Vector3)(ref targetRelativePosition)).sqrMagnitude;
		float num3 = num2 * num2 - 4f * num * sqrMagnitude2;
		if (num3 > 0f)
		{
			float num4 = (0f - num2 + Mathf.Sqrt(num3)) / (2f * num);
			float num5 = (0f - num2 - Mathf.Sqrt(num3)) / (2f * num);
			if (num4 > 0f)
			{
				if (num5 > 0f)
				{
					return Mathf.Min(num4, num5);
				}
				return num4;
			}
			return Mathf.Max(num5, 0f);
		}
		if (num3 < 0f)
		{
			return 0f;
		}
		return Mathf.Max((0f - num2) / (2f * num), 0f);
	}
}
