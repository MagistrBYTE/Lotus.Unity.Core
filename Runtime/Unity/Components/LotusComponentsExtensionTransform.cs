//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема компонентов
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusComponentsExtensionTransform.cs
*		Методы расширения функциональности компонента Transform.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 04.04.2021
//=====================================================================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreUnityComponent
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий методы расширений функциональности компонента Transform
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XExtensionTransform
		{
			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка позиции игрового объекта по X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="x">Позиция по X</param>
			//---------------------------------------------------------------------------------------------------------
			public static void SetX(this Transform @this, Single x)
			{
#if UNITY_EDITOR
				if (@this == null)
				{
					return;
				}
#endif
				Vector3 pos = @this.position;
				pos.x = x;
				@this.position = pos;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка позиции игрового объекта по Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="y">Позиция по Y</param>
			//---------------------------------------------------------------------------------------------------------
			public static void SetY(this Transform @this, Single y)
			{
#if UNITY_EDITOR
				if (@this == null)
				{
					return;
				}
#endif
				Vector3 pos = @this.position;
				pos.y = y;
				@this.position = pos;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка позиции игрового объекта по Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="z">Координата по Z</param>
			//---------------------------------------------------------------------------------------------------------
			public static void SetZ(this Transform @this, Single z)
			{
#if UNITY_EDITOR
				if (@this == null)
				{
					return;
				}
#endif
				Vector3 pos = @this.position;
				pos.z = z;
				@this.position = pos;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Наклон или поворот игрового объекта относительно правого вектора (по оси X)
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="x">Угол поворота в градусах</param>
			//---------------------------------------------------------------------------------------------------------
			public static void SetPitch(this Transform @this, Single x)
			{
#if UNITY_EDITOR
				if (@this == null)
				{
					return;
				}
#endif
				Vector3 pos = @this.eulerAngles;
				pos.x = x;
				@this.eulerAngles = pos;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Рыскание или поворот игрового объекта относительно верхнего вектора (по оси Y)
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="y">Угол поворота в градусах</param>
			//---------------------------------------------------------------------------------------------------------
			public static void SetYaw(this Transform @this, Single y)
			{
#if UNITY_EDITOR
				if (@this == null)
				{
					return;
				}
#endif
				Vector3 pos = @this.eulerAngles;
				pos.y = y;
				@this.eulerAngles = pos;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вращение или поворот игрового объекта относительно вектора направления (по оси Z)
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="z">Угол поворота в градусах</param>
			//---------------------------------------------------------------------------------------------------------
			public static void SetRoll(this Transform @this, Single z)
			{
#if UNITY_EDITOR
				if (@this == null)
				{
					return;
				}
#endif
				Vector3 pos = @this.eulerAngles;
				pos.z = z;
				@this.eulerAngles = pos;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Абсолютная глубина компонента в иерархии
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <returns>Абсолютная глубина компонента в иерархии</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Int32 AbsoluteDepth(this Transform @this)
			{
				Int32 depth = 0;
				Transform current_transform;
				if(@this.parent != null)
				{
					depth = 1;
					current_transform = @this.parent;
					if(current_transform.parent != null)
					{
						depth = 2;
						current_transform = @this.parent;
						if (current_transform.parent != null)
						{
							depth = 3;
							current_transform = @this.parent;
							if (current_transform.parent != null)
							{
								depth = 4;
								current_transform = @this.parent;
								if (current_transform.parent != null)
								{
									depth = 5;
									current_transform = @this.parent;
									if (current_transform.parent != null)
									{
										depth = 6;
										current_transform = @this.parent;
										if (current_transform.parent != null)
										{
											depth = 7;
											current_transform = @this.parent;
											if (current_transform.parent != null)
											{
												depth = 8;
											}
										}
									}
								}
							}
						}
					}
				}

				return depth;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вращение по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время полного поворота</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RotateX(this Transform @this, Single duration)
			{
				Single rotation = @this.eulerAngles.x;
				rotation += 360 * Time.unscaledDeltaTime / duration;
				@this.eulerAngles = new Vector3(rotation, @this.eulerAngles.y, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вращение по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время полного поворота</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RotateY(this Transform @this, Single duration)
			{
				Single rotation = @this.eulerAngles.y;
				rotation += 360 * Time.unscaledDeltaTime / duration;
				@this.eulerAngles = new Vector3(@this.eulerAngles.x, rotation, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Вращение по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время полного поворота</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RotateZ(this Transform @this, Single duration)
			{
				Single rotation = @this.eulerAngles.z;
				rotation += 360 * Time.unscaledDeltaTime / duration;
				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, rotation);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение компонента указанного типа от непосредственного родителя
			/// </summary>
			/// <typeparam name="TComponent">Тип компонента</typeparam>
			/// <param name="this">Компонент трансформации</param>
			/// <returns>Компонент</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TComponent GetComponentFromParent<TComponent>(this Transform @this) where TComponent : Component
			{
				if (@this.parent != null)
				{
					return (@this.transform.parent.GetComponent<TComponent>());
				}

				return (null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение компонента указанного типа от непосредственного родителя или его родителя
			/// </summary>
			/// <typeparam name="TComponent">Тип компонента</typeparam>
			/// <param name="this">Компонент трансформации</param>
			/// <returns>Компонент</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TComponent GetComponentFromParentOrHisParent<TComponent>(this Transform @this) where TComponent : Component
			{
				if (@this.parent != null)
				{
					TComponent сomponent = @this.parent.GetComponent<TComponent>();
					if (сomponent != null)
					{
						return (сomponent);
					}
					else
					{
						if (@this.parent.parent != null)
						{
							return (@this.parent.parent.GetComponent<TComponent>());
						}
					}
				}

				return (null);
			}
			#endregion

			#region ======================================= МЕТОДЫ Move ===============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Старт перемещению компонента трансформации к указанной позиции
			/// </summary>
			/// <param name="this">Пользовательский компонент</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <param name="easing_type">Тип функции скорости</param>
			//---------------------------------------------------------------------------------------------------------
			public static void MoveTo(this MonoBehaviour @this, Single duration, Vector3 target_position, TEasingType easing_type)
			{
				MoveTo(@this, @this.transform, duration, target_position, easing_type);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Старт перемещению компонента трансформации к указанной позиции
			/// </summary>
			/// <param name="this">Пользовательский компонент</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <param name="easing_type">Тип функции скорости</param>
			/// <param name="on_completed">Обработчик события окончания перемещения</param>
			//---------------------------------------------------------------------------------------------------------
			public static void MoveTo(this MonoBehaviour @this, Single duration, Vector3 target_position, TEasingType easing_type, Action on_completed)
			{
				MoveTo(@this, @this.transform, duration, target_position, easing_type, on_completed);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Старт перемещению компонента трансформации к указанной позиции
			/// </summary>
			/// <param name="this">Пользовательский компонент</param>
			/// <param name="transform">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <param name="easing_type">Тип функции скорости</param>
			//---------------------------------------------------------------------------------------------------------
			public static void MoveTo(this MonoBehaviour @this, Transform transform, Single duration, Vector3 target_position, TEasingType easing_type)
			{
				switch (easing_type)
				{
					case TEasingType.Linear:
						{
							@this.StartCoroutine(MoveToLinearIteration(transform, duration, target_position));
						}
						break;
					case TEasingType.QuadIn:
						{
							@this.StartCoroutine(MoveToQuadInIteration(transform, duration, target_position));
						}
						break;
					case TEasingType.QuadOut:
						{
							@this.StartCoroutine(MoveToQuadOutIteration(transform, duration, target_position));
						}
						break;
					case TEasingType.QuadInOut:
						{
							@this.StartCoroutine(MoveToQuadInOutIteration(transform, duration, target_position));
						}
						break;
					case TEasingType.CubeIn:
						{
							@this.StartCoroutine(MoveToCubeInIteration(transform, duration, target_position));
						}
						break;
					case TEasingType.CubeOut:
						{
							@this.StartCoroutine(MoveToCubeOutIteration(transform, duration, target_position));
						}
						break;
					case TEasingType.CubeInOut:
						{
							@this.StartCoroutine(MoveToCubeInOutIteration(transform, duration, target_position));
						}
						break;
					case TEasingType.BackIn:
						{
							@this.StartCoroutine(MoveToBackInIteration(transform, duration, target_position));
						}
						break;
					case TEasingType.BackOut:
						{
							@this.StartCoroutine(MoveToBackOutIteration(transform, duration, target_position));
						}
						break;
					case TEasingType.BackInOut:
						{
							@this.StartCoroutine(MoveToBackInOutIteration(transform, duration, target_position));
						}
						break;
					case TEasingType.ExpoIn:
						{
							@this.StartCoroutine(MoveToExpoInIteration(transform, duration, target_position));
						}
						break;
					case TEasingType.ExpoOut:
						{
							@this.StartCoroutine(MoveToExpoOutIteration(transform, duration, target_position));
						}
						break;
					case TEasingType.ExpoInOut:
						{
							@this.StartCoroutine(MoveToExpoInOutIteration(transform, duration, target_position));
						}
						break;
					case TEasingType.SineIn:
						{
							@this.StartCoroutine(MoveToSineInIteration(transform, duration, target_position));
						}
						break;
					case TEasingType.SineOut:
						{
							@this.StartCoroutine(MoveToSineOutIteration(transform, duration, target_position));
						}
						break;
					case TEasingType.SineInOut:
						{
							@this.StartCoroutine(MoveToSineInOutIteration(transform, duration, target_position));
						}
						break;
					case TEasingType.ElasticIn:
						{
							@this.StartCoroutine(MoveToElasticInIteration(transform, duration, target_position));
						}
						break;
					case TEasingType.ElasticOut:
						{
							@this.StartCoroutine(MoveToElasticOutIteration(transform, duration, target_position));
						}
						break;
					case TEasingType.ElasticInOut:
						{
							@this.StartCoroutine(MoveToElasticInOutIteration(transform, duration, target_position));
						}
						break;
					case TEasingType.BounceIn:
						{
							@this.StartCoroutine(MoveToBounceInIteration(transform, duration, target_position));
						}
						break;
					case TEasingType.BounceOut:
						{
							@this.StartCoroutine(MoveToBounceOutIteration(transform, duration, target_position));
						}
						break;
					case TEasingType.BounceInOut:
						{
							@this.StartCoroutine(MoveToBounceInOutIteration(transform, duration, target_position));
						}
						break;
					default:
						break;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Старт перемещению компонента трансформации к указанной позиции
			/// </summary>
			/// <param name="this">Пользовательский компонент</param>
			/// <param name="transform">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <param name="easing_type">Тип функции скорости</param>
			/// <param name="on_completed">Обработчик события окончания перемещения</param>
			//---------------------------------------------------------------------------------------------------------
			public static void MoveTo(this MonoBehaviour @this, Transform transform, Single duration, Vector3 target_position, TEasingType easing_type, Action on_completed)
			{
				switch (easing_type)
				{
					case TEasingType.Linear:
						{
							@this.StartCoroutine(MoveToLinearIteration(transform, duration, target_position, on_completed));
						}
						break;
					case TEasingType.QuadIn:
						{
							@this.StartCoroutine(MoveToQuadInIteration(transform, duration, target_position, on_completed));
						}
						break;
					case TEasingType.QuadOut:
						{
							@this.StartCoroutine(MoveToQuadOutIteration(transform, duration, target_position, on_completed));
						}
						break;
					case TEasingType.QuadInOut:
						{
							@this.StartCoroutine(MoveToQuadInOutIteration(transform, duration, target_position, on_completed));
						}
						break;
					case TEasingType.CubeIn:
						{
							@this.StartCoroutine(MoveToCubeInIteration(transform, duration, target_position, on_completed));
						}
						break;
					case TEasingType.CubeOut:
						{
							@this.StartCoroutine(MoveToCubeOutIteration(transform, duration, target_position, on_completed));
						}
						break;
					case TEasingType.CubeInOut:
						{
							@this.StartCoroutine(MoveToCubeInOutIteration(transform, duration, target_position, on_completed));
						}
						break;
					case TEasingType.BackIn:
						{
							@this.StartCoroutine(MoveToBackInIteration(transform, duration, target_position, on_completed));
						}
						break;
					case TEasingType.BackOut:
						{
							@this.StartCoroutine(MoveToBackOutIteration(transform, duration, target_position, on_completed));
						}
						break;
					case TEasingType.BackInOut:
						{
							@this.StartCoroutine(MoveToBackInOutIteration(transform, duration, target_position, on_completed));
						}
						break;
					case TEasingType.ExpoIn:
						{
							@this.StartCoroutine(MoveToExpoInIteration(transform, duration, target_position, on_completed));
						}
						break;
					case TEasingType.ExpoOut:
						{
							@this.StartCoroutine(MoveToExpoOutIteration(transform, duration, target_position, on_completed));
						}
						break;
					case TEasingType.ExpoInOut:
						{
							@this.StartCoroutine(MoveToExpoInOutIteration(transform, duration, target_position, on_completed));
						}
						break;
					case TEasingType.SineIn:
						{
							@this.StartCoroutine(MoveToSineInIteration(transform, duration, target_position, on_completed));
						}
						break;
					case TEasingType.SineOut:
						{
							@this.StartCoroutine(MoveToSineOutIteration(transform, duration, target_position, on_completed));
						}
						break;
					case TEasingType.SineInOut:
						{
							@this.StartCoroutine(MoveToSineInOutIteration(transform, duration, target_position, on_completed));
						}
						break;
					case TEasingType.ElasticIn:
						{
							@this.StartCoroutine(MoveToElasticInIteration(transform, duration, target_position, on_completed));
						}
						break;
					case TEasingType.ElasticOut:
						{
							@this.StartCoroutine(MoveToElasticOutIteration(transform, duration, target_position, on_completed));
						}
						break;
					case TEasingType.ElasticInOut:
						{
							@this.StartCoroutine(MoveToElasticInOutIteration(transform, duration, target_position, on_completed));
						}
						break;
					case TEasingType.BounceIn:
						{
							@this.StartCoroutine(MoveToBounceInIteration(transform, duration, target_position, on_completed));
						}
						break;
					case TEasingType.BounceOut:
						{
							@this.StartCoroutine(MoveToBounceOutIteration(transform, duration, target_position, on_completed));
						}
						break;
					case TEasingType.BounceInOut:
						{
							@this.StartCoroutine(MoveToBounceInOutIteration(transform, duration, target_position, on_completed));
						}
						break;
					default:
						break;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToLinearIteration(this Transform @this, Single duration, Vector3 target_position)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.Linear(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <param name="on_completed">Обработчик события окончания перемещения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToLinearIteration(this Transform @this, Single duration, Vector3 target_position, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.Linear(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToQuadInIteration(this Transform @this, Single duration, Vector3 target_position)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.QuadIn(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <param name="on_completed">Обработчик события окончания перемещения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToQuadInIteration(this Transform @this, Single duration, Vector3 target_position, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.QuadIn(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToQuadOutIteration(this Transform @this, Single duration, Vector3 target_position)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.QuadOut(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <param name="on_completed">Обработчик события окончания перемещения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToQuadOutIteration(this Transform @this, Single duration, Vector3 target_position, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.QuadOut(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToQuadInOutIteration(this Transform @this, Single duration, Vector3 target_position)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.QuadInOut(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <param name="on_completed">Обработчик события окончания перемещения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToQuadInOutIteration(this Transform @this, Single duration, Vector3 target_position, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.QuadInOut(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToCubeInIteration(this Transform @this, Single duration, Vector3 target_position)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.CubeIn(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <param name="on_completed">Обработчик события окончания перемещения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToCubeInIteration(this Transform @this, Single duration, Vector3 target_position, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.CubeIn(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToCubeOutIteration(this Transform @this, Single duration, Vector3 target_position)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.CubeOut(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <param name="on_completed">Обработчик события окончания перемещения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToCubeOutIteration(this Transform @this, Single duration, Vector3 target_position, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.CubeOut(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToCubeInOutIteration(this Transform @this, Single duration, Vector3 target_position)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.CubeInOut(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <param name="on_completed">Обработчик события окончания перемещения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToCubeInOutIteration(this Transform @this, Single duration, Vector3 target_position, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.CubeInOut(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToBackInIteration(this Transform @this, Single duration, Vector3 target_position)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.BackIn(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <param name="on_completed">Обработчик события окончания перемещения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToBackInIteration(this Transform @this, Single duration, Vector3 target_position, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.BackIn(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToBackOutIteration(this Transform @this, Single duration, Vector3 target_position)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.BackOut(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <param name="on_completed">Обработчик события окончания перемещения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToBackOutIteration(this Transform @this, Single duration, Vector3 target_position, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.BackOut(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToBackInOutIteration(this Transform @this, Single duration, Vector3 target_position)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.BackInOut(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <param name="on_completed">Обработчик события окончания перемещения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToBackInOutIteration(this Transform @this, Single duration, Vector3 target_position, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.BackInOut(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToExpoInIteration(this Transform @this, Single duration, Vector3 target_position)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.ExpoIn(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <param name="on_completed">Обработчик события окончания перемещения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToExpoInIteration(this Transform @this, Single duration, Vector3 target_position, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.ExpoIn(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToExpoOutIteration(this Transform @this, Single duration, Vector3 target_position)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.ExpoOut(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <param name="on_completed">Обработчик события окончания перемещения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToExpoOutIteration(this Transform @this, Single duration, Vector3 target_position, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.ExpoOut(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToExpoInOutIteration(this Transform @this, Single duration, Vector3 target_position)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.ExpoInOut(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <param name="on_completed">Обработчик события окончания перемещения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToExpoInOutIteration(this Transform @this, Single duration, Vector3 target_position, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.ExpoInOut(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToSineInIteration(this Transform @this, Single duration, Vector3 target_position)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.SineIn(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <param name="on_completed">Обработчик события окончания перемещения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToSineInIteration(this Transform @this, Single duration, Vector3 target_position, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.SineIn(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToSineOutIteration(this Transform @this, Single duration, Vector3 target_position)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.SineOut(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <param name="on_completed">Обработчик события окончания перемещения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToSineOutIteration(this Transform @this, Single duration, Vector3 target_position, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.SineOut(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToSineInOutIteration(this Transform @this, Single duration, Vector3 target_position)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.SineInOut(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <param name="on_completed">Обработчик события окончания перемещения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToSineInOutIteration(this Transform @this, Single duration, Vector3 target_position, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.SineInOut(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToElasticInIteration(this Transform @this, Single duration, Vector3 target_position)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.ElasticIn(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <param name="on_completed">Обработчик события окончания перемещения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToElasticInIteration(this Transform @this, Single duration, Vector3 target_position, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.ElasticIn(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToElasticOutIteration(this Transform @this, Single duration, Vector3 target_position)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.ElasticOut(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <param name="on_completed">Обработчик события окончания перемещения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToElasticOutIteration(this Transform @this, Single duration, Vector3 target_position, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.ElasticOut(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToElasticInOutIteration(this Transform @this, Single duration, Vector3 target_position)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.ElasticInOut(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <param name="on_completed">Обработчик события окончания перемещения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToElasticInOutIteration(this Transform @this, Single duration, Vector3 target_position, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.ElasticInOut(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToBounceInIteration(this Transform @this, Single duration, Vector3 target_position)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.BounceIn(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <param name="on_completed">Обработчик события окончания перемещения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToBounceInIteration(this Transform @this, Single duration, Vector3 target_position, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.BounceIn(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToBounceOutIteration(this Transform @this, Single duration, Vector3 target_position)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.BounceOut(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <param name="on_completed">Обработчик события окончания перемещения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToBounceOutIteration(this Transform @this, Single duration, Vector3 target_position, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.BounceOut(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToBounceInOutIteration(this Transform @this, Single duration, Vector3 target_position)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.BounceInOut(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма перемещения компонента трансформации
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время перемещения</param>
			/// <param name="target_position">Целевая позиция</param>
			/// <param name="on_completed">Обработчик события окончания перемещения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator MoveToBounceInOutIteration(this Transform @this, Single duration, Vector3 target_position, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Vector3 start_position = @this.position;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.position = XEasing.BounceInOut(ref start_position, ref target_position, time);
					yield return null;
				}

				@this.position = target_position;

				on_completed();
			}
			#endregion

			#region ======================================= МЕТОДЫ Rotation ===========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Старт вращению компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Пользовательский компонент</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Кватернион вращения</param>
			/// <param name="easing_type">Тип функции скорости</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RotationTo(this MonoBehaviour @this, Single duration, Quaternion target, TEasingType easing_type)
			{
				RotationTo(@this, @this.transform, duration, target, easing_type);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Старт вращению компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Пользовательский компонент</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Кватернион вращения</param>
			/// <param name="easing_type">Тип функции скорости</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RotationTo(this MonoBehaviour @this, Single duration, Quaternion target, TEasingType easing_type, Action on_completed)
			{
				RotationTo(@this, @this.transform, duration, target, easing_type, on_completed);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Старт вращению компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Пользовательский компонент</param>
			/// <param name="transform">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Кватернион вращения</param>
			/// <param name="easing_type">Тип функции скорости</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RotationTo(this MonoBehaviour @this, Transform transform, Single duration, Quaternion target, TEasingType easing_type)
			{
				switch (easing_type)
				{
					case TEasingType.Linear:
						{
							@this.StartCoroutine(RotationToLinearIteration(transform, duration, target));
						}
						break;
					case TEasingType.QuadIn:
						{
							@this.StartCoroutine(RotationToQuadInIteration(transform, duration, target));
						}
						break;
					case TEasingType.QuadOut:
						{
							@this.StartCoroutine(RotationToQuadOutIteration(transform, duration, target));
						}
						break;
					case TEasingType.QuadInOut:
						{
							@this.StartCoroutine(RotationToQuadInOutIteration(transform, duration, target));
						}
						break;
					case TEasingType.CubeIn:
						{
							@this.StartCoroutine(RotationToCubeInIteration(transform, duration, target));
						}
						break;
					case TEasingType.CubeOut:
						{
							@this.StartCoroutine(RotationToCubeOutIteration(transform, duration, target));
						}
						break;
					case TEasingType.CubeInOut:
						{
							@this.StartCoroutine(RotationToCubeInOutIteration(transform, duration, target));
						}
						break;
					case TEasingType.BackIn:
						{
							@this.StartCoroutine(RotationToBackInIteration(transform, duration, target));
						}
						break;
					case TEasingType.BackOut:
						{
							@this.StartCoroutine(RotationToBackOutIteration(transform, duration, target));
						}
						break;
					case TEasingType.BackInOut:
						{
							@this.StartCoroutine(RotationToBackInOutIteration(transform, duration, target));
						}
						break;
					case TEasingType.ExpoIn:
						{
							@this.StartCoroutine(RotationToExpoInIteration(transform, duration, target));
						}
						break;
					case TEasingType.ExpoOut:
						{
							@this.StartCoroutine(RotationToExpoOutIteration(transform, duration, target));
						}
						break;
					case TEasingType.ExpoInOut:
						{
							@this.StartCoroutine(RotationToExpoInOutIteration(transform, duration, target));
						}
						break;
					case TEasingType.SineIn:
						{
							@this.StartCoroutine(RotationToSineInIteration(transform, duration, target));
						}
						break;
					case TEasingType.SineOut:
						{
							@this.StartCoroutine(RotationToSineOutIteration(transform, duration, target));
						}
						break;
					case TEasingType.SineInOut:
						{
							@this.StartCoroutine(RotationToSineInOutIteration(transform, duration, target));
						}
						break;
					case TEasingType.ElasticIn:
						{
							@this.StartCoroutine(RotationToElasticInIteration(transform, duration, target));
						}
						break;
					case TEasingType.ElasticOut:
						{
							@this.StartCoroutine(RotationToElasticOutIteration(transform, duration, target));
						}
						break;
					case TEasingType.ElasticInOut:
						{
							@this.StartCoroutine(RotationToElasticInOutIteration(transform, duration, target));
						}
						break;
					case TEasingType.BounceIn:
						{
							@this.StartCoroutine(RotationToBounceInIteration(transform, duration, target));
						}
						break;
					case TEasingType.BounceOut:
						{
							@this.StartCoroutine(RotationToBounceOutIteration(transform, duration, target));
						}
						break;
					case TEasingType.BounceInOut:
						{
							@this.StartCoroutine(RotationToBounceInOutIteration(transform, duration, target));
						}
						break;
					default:
						break;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Старт вращению компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Пользовательский компонент</param>
			/// <param name="transform">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Кватернион вращения</param>
			/// <param name="easing_type">Тип функции скорости</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RotationTo(this MonoBehaviour @this, Transform transform, Single duration, Quaternion target, TEasingType easing_type, Action on_completed)
			{
				switch (easing_type)
				{
					case TEasingType.Linear:
						{
							@this.StartCoroutine(RotationToLinearIteration(transform, duration, target, on_completed));
						}
						break;
					case TEasingType.QuadIn:
						{
							@this.StartCoroutine(RotationToQuadInIteration(transform, duration, target, on_completed));
						}
						break;
					case TEasingType.QuadOut:
						{
							@this.StartCoroutine(RotationToQuadOutIteration(transform, duration, target, on_completed));
						}
						break;
					case TEasingType.QuadInOut:
						{
							@this.StartCoroutine(RotationToQuadInOutIteration(transform, duration, target, on_completed));
						}
						break;
					case TEasingType.CubeIn:
						{
							@this.StartCoroutine(RotationToCubeInIteration(transform, duration, target, on_completed));
						}
						break;
					case TEasingType.CubeOut:
						{
							@this.StartCoroutine(RotationToCubeOutIteration(transform, duration, target, on_completed));
						}
						break;
					case TEasingType.CubeInOut:
						{
							@this.StartCoroutine(RotationToCubeInOutIteration(transform, duration, target, on_completed));
						}
						break;
					case TEasingType.BackIn:
						{
							@this.StartCoroutine(RotationToBackInIteration(transform, duration, target, on_completed));
						}
						break;
					case TEasingType.BackOut:
						{
							@this.StartCoroutine(RotationToBackOutIteration(transform, duration, target, on_completed));
						}
						break;
					case TEasingType.BackInOut:
						{
							@this.StartCoroutine(RotationToBackInOutIteration(transform, duration, target, on_completed));
						}
						break;
					case TEasingType.ExpoIn:
						{
							@this.StartCoroutine(RotationToExpoInIteration(transform, duration, target, on_completed));
						}
						break;
					case TEasingType.ExpoOut:
						{
							@this.StartCoroutine(RotationToExpoOutIteration(transform, duration, target, on_completed));
						}
						break;
					case TEasingType.ExpoInOut:
						{
							@this.StartCoroutine(RotationToExpoInOutIteration(transform, duration, target, on_completed));
						}
						break;
					case TEasingType.SineIn:
						{
							@this.StartCoroutine(RotationToSineInIteration(transform, duration, target, on_completed));
						}
						break;
					case TEasingType.SineOut:
						{
							@this.StartCoroutine(RotationToSineOutIteration(transform, duration, target, on_completed));
						}
						break;
					case TEasingType.SineInOut:
						{
							@this.StartCoroutine(RotationToSineInOutIteration(transform, duration, target, on_completed));
						}
						break;
					case TEasingType.ElasticIn:
						{
							@this.StartCoroutine(RotationToElasticInIteration(transform, duration, target, on_completed));
						}
						break;
					case TEasingType.ElasticOut:
						{
							@this.StartCoroutine(RotationToElasticOutIteration(transform, duration, target, on_completed));
						}
						break;
					case TEasingType.ElasticInOut:
						{
							@this.StartCoroutine(RotationToElasticInOutIteration(transform, duration, target, on_completed));
						}
						break;
					case TEasingType.BounceIn:
						{
							@this.StartCoroutine(RotationToBounceInIteration(transform, duration, target, on_completed));
						}
						break;
					case TEasingType.BounceOut:
						{
							@this.StartCoroutine(RotationToBounceOutIteration(transform, duration, target, on_completed));
						}
						break;
					case TEasingType.BounceInOut:
						{
							@this.StartCoroutine(RotationToBounceInOutIteration(transform, duration, target, on_completed));
						}
						break;
					default:
						break;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Кватернион вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToLinearIteration(this Transform @this, Single duration, Quaternion target)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, time);
					yield return null;
				}

				@this.rotation = target;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Кватернион вращения</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToLinearIteration(this Transform @this, Single duration, Quaternion target, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, time);
					yield return null;
				}

				@this.rotation = target;
				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Кватернион вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToQuadInIteration(this Transform @this, Single duration, Quaternion target)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, time * time);
					yield return null;
				}

				@this.rotation = target;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Кватернион вращения</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToQuadInIteration(this Transform @this, Single duration, Quaternion target, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, time * time);
					yield return null;
				}

				@this.rotation = target;
				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Кватернион вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToQuadOutIteration(this Transform @this, Single duration, Quaternion target)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.QuadOut(0, 1, time));
					yield return null;
				}

				@this.rotation = target;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Кватернион вращения</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToQuadOutIteration(this Transform @this, Single duration, Quaternion target, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.QuadOut(0, 1, time));
					yield return null;
				}

				@this.rotation = target;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Кватернион вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToQuadInOutIteration(this Transform @this, Single duration, Quaternion target)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.QuadInOut(0, 1, time));
					yield return null;
				}

				@this.rotation = target;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Кватернион вращения</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToQuadInOutIteration(this Transform @this, Single duration, Quaternion target, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.QuadInOut(0, 1, time));
					yield return null;
				}

				@this.rotation = target;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Кватернион вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToCubeInIteration(this Transform @this, Single duration, Quaternion target)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.CubeIn(0, 1, time));
					yield return null;
				}

				@this.rotation = target;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Кватернион вращения</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToCubeInIteration(this Transform @this, Single duration, Quaternion target, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.CubeIn(0, 1, time));
					yield return null;
				}

				@this.rotation = target;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Кватернион вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToCubeOutIteration(this Transform @this, Single duration, Quaternion target)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.CubeOut(0, 1, time));
					yield return null;
				}

				@this.rotation = target;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Кватернион вращения</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToCubeOutIteration(this Transform @this, Single duration, Quaternion target, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.CubeOut(0, 1, time));
					yield return null;
				}

				@this.rotation = target;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Кватернион вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToCubeInOutIteration(this Transform @this, Single duration, Quaternion target)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.CubeInOut(0, 1, time));
					yield return null;
				}

				@this.rotation = target;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Кватернион вращения</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToCubeInOutIteration(this Transform @this, Single duration, Quaternion target, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.CubeInOut(0, 1, time));
					yield return null;
				}

				@this.rotation = target;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Кватернион вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToBackInIteration(this Transform @this, Single duration, Quaternion target)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.BackIn(0, 1, time));
					yield return null;
				}

				@this.rotation = target;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Кватернион вращения</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToBackInIteration(this Transform @this, Single duration, Quaternion target, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.BackIn(0, 1, time));
					yield return null;
				}

				@this.rotation = target;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Кватернион вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToBackOutIteration(this Transform @this, Single duration, Quaternion target)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.BackOut(0, 1, time));
					yield return null;
				}

				@this.rotation = target;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Кватернион вращения</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToBackOutIteration(this Transform @this, Single duration, Quaternion target, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.BackOut(0, 1, time));
					yield return null;
				}

				@this.rotation = target;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Кватернион вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToBackInOutIteration(this Transform @this, Single duration, Quaternion target)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.BackInOut(0, 1, time));
					yield return null;
				}

				@this.rotation = target;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Кватернион вращения</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToBackInOutIteration(this Transform @this, Single duration, Quaternion target, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.BackInOut(0, 1, time));
					yield return null;
				}

				@this.rotation = target;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Кватернион вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToExpoInIteration(this Transform @this, Single duration, Quaternion target)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.ExpoIn(0, 1, time));
					yield return null;
				}

				@this.rotation = target;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Кватернион вращения</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToExpoInIteration(this Transform @this, Single duration, Quaternion target, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.ExpoIn(0, 1, time));
					yield return null;
				}

				@this.rotation = target;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Кватернион вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToExpoOutIteration(this Transform @this, Single duration, Quaternion target)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.ExpoOut(0, 1, time));
					yield return null;
				}

				@this.rotation = target;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Кватернион вращения</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToExpoOutIteration(this Transform @this, Single duration, Quaternion target, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.ExpoOut(0, 1, time));
					yield return null;
				}

				@this.rotation = target;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Кватернион вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToExpoInOutIteration(this Transform @this, Single duration, Quaternion target)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.ExpoInOut(0, 1, time));
					yield return null;
				}

				@this.rotation = target;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Кватернион вращения</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToExpoInOutIteration(this Transform @this, Single duration, Quaternion target, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.ExpoInOut(0, 1, time));
					yield return null;
				}

				@this.rotation = target;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Целевой кватернион</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToSineInIteration(this Transform @this, Single duration, Quaternion target)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.SineIn(0, 1, time));
					yield return null;
				}

				@this.rotation = target;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Целевой кватернион</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToSineInIteration(this Transform @this, Single duration, Quaternion target, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.SineIn(0, 1, time));
					yield return null;
				}

				@this.rotation = target;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Целевой кватернион</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToSineOutIteration(this Transform @this, Single duration, Quaternion target)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.SineOut(0, 1, time));
					yield return null;
				}

				@this.rotation = target;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Целевой кватернион</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToSineOutIteration(this Transform @this, Single duration, Quaternion target, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.SineOut(0, 1, time));
					yield return null;
				}

				@this.rotation = target;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Целевой кватернион</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToSineInOutIteration(this Transform @this, Single duration, Quaternion target)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.SineInOut(0, 1, time));
					yield return null;
				}

				@this.rotation = target;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Целевой кватернион</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToSineInOutIteration(this Transform @this, Single duration, Quaternion target, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.SineInOut(0, 1, time));
					yield return null;
				}

				@this.rotation = target;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Целевой кватернион</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToElasticInIteration(this Transform @this, Single duration, Quaternion target)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.ElasticIn(0, 1, time));
					yield return null;
				}

				@this.rotation = target;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Целевой кватернион</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToElasticInIteration(this Transform @this, Single duration, Quaternion target, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.ElasticIn(0, 1, time));
					yield return null;
				}

				@this.rotation = target;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Целевой кватернион</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToElasticOutIteration(this Transform @this, Single duration, Quaternion target)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.ElasticOut(0, 1, time));
					yield return null;
				}

				@this.rotation = target;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Целевой кватернион</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToElasticOutIteration(this Transform @this, Single duration, Quaternion target, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.ElasticOut(0, 1, time));
					yield return null;
				}

				@this.rotation = target;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Целевой кватернион</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToElasticInOutIteration(this Transform @this, Single duration, Quaternion target)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.ElasticInOut(0, 1, time));
					yield return null;
				}

				@this.rotation = target;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Целевой кватернион</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToElasticInOutIteration(this Transform @this, Single duration, Quaternion target, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.ElasticInOut(0, 1, time));
					yield return null;
				}

				@this.rotation = target;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Целевой кватернион</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToBounceInIteration(this Transform @this, Single duration, Quaternion target)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.BounceIn(0, 1, time));
					yield return null;
				}

				@this.rotation = target;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Целевой кватернион</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToBounceInIteration(this Transform @this, Single duration, Quaternion target, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.BounceIn(0, 1, time));
					yield return null;
				}

				@this.rotation = target;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Целевой кватернион</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToBounceOutIteration(this Transform @this, Single duration, Quaternion target)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.BounceOut(0, 1, time));
					yield return null;
				}

				@this.rotation = target;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Целевой кватернион</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToBounceOutIteration(this Transform @this, Single duration, Quaternion target, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.BounceOut(0, 1, time));
					yield return null;
				}

				@this.rotation = target;

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Целевой кватернион</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToBounceInOutIteration(this Transform @this, Single duration, Quaternion target)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.BounceInOut(0, 1, time));
					yield return null;
				}

				@this.rotation = target;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации к указанному кватерниону
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target">Целевой кватернион</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToBounceInOutIteration(this Transform @this, Single duration, Quaternion target, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Quaternion start_rotation = @this.rotation;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.rotation = Quaternion.Lerp(start_rotation, target, XEasing.BounceInOut(0, 1, time));
					yield return null;
				}

				@this.rotation = target;

				on_completed();
			}
			#endregion

			#region ======================================= МЕТОДЫ RotationX ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Старт вращению компонента трансформации к указанному углу по оси X
			/// </summary>
			/// <param name="this">Пользовательский компонент</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="easing_type">Тип функции скорости</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RotationToX(this MonoBehaviour @this, Single duration, Single target_angle, TEasingType easing_type)
			{
				RotationToX(@this, @this.transform, duration, target_angle, easing_type);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Старт вращению компонента трансформации к указанному углу по оси X
			/// </summary>
			/// <param name="this">Пользовательский компонент</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="easing_type">Тип функции скорости</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RotationToX(this MonoBehaviour @this, Single duration, Single target_angle, TEasingType easing_type, Action on_completed)
			{
				RotationToX(@this, @this.transform, duration, target_angle, easing_type, on_completed);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Старт вращению компонента трансформации к указанному углу по оси X
			/// </summary>
			/// <param name="this">Пользовательский компонент</param>
			/// <param name="transform">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="easing_type">Тип функции скорости</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RotationToX(this MonoBehaviour @this, Transform transform, Single duration, Single target_angle, TEasingType easing_type)
			{
				switch (easing_type)
				{
					case TEasingType.Linear:
						{
							@this.StartCoroutine(RotationToXLinearIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.QuadIn:
						{
							@this.StartCoroutine(RotationToXQuadInIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.QuadOut:
						{
							@this.StartCoroutine(RotationToXQuadOutIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.QuadInOut:
						{
							@this.StartCoroutine(RotationToXQuadInOutIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.CubeIn:
						{
							@this.StartCoroutine(RotationToXCubeInIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.CubeOut:
						{
							@this.StartCoroutine(RotationToXCubeOutIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.CubeInOut:
						{
							@this.StartCoroutine(RotationToXCubeInOutIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.BackIn:
						{
							@this.StartCoroutine(RotationToXBackInIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.BackOut:
						{
							@this.StartCoroutine(RotationToXBackOutIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.BackInOut:
						{
							@this.StartCoroutine(RotationToXBackInOutIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.ExpoIn:
						{
							@this.StartCoroutine(RotationToXExpoInIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.ExpoOut:
						{
							@this.StartCoroutine(RotationToXExpoOutIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.ExpoInOut:
						{
							@this.StartCoroutine(RotationToXExpoInOutIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.SineIn:
						{
							@this.StartCoroutine(RotationToXSineInIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.SineOut:
						{
							@this.StartCoroutine(RotationToXSineOutIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.SineInOut:
						{
							@this.StartCoroutine(RotationToXSineInOutIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.ElasticIn:
						{
							@this.StartCoroutine(RotationToXElasticInIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.ElasticOut:
						{
							@this.StartCoroutine(RotationToXElasticOutIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.ElasticInOut:
						{
							@this.StartCoroutine(RotationToXElasticInOutIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.BounceIn:
						{
							@this.StartCoroutine(RotationToXBounceInIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.BounceOut:
						{
							@this.StartCoroutine(RotationToXBounceOutIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.BounceInOut:
						{
							@this.StartCoroutine(RotationToXBounceInOutIteration(transform, duration, target_angle));
						}
						break;
					default:
						break;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Старт вращению компонента трансформации к указанному углу по оси X
			/// </summary>
			/// <param name="this">Пользовательский компонент</param>
			/// <param name="transform">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="easing_type">Тип функции скорости</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RotationToX(this MonoBehaviour @this, Transform transform, Single duration, Single target_angle, TEasingType easing_type, Action on_completed)
			{
				switch (easing_type)
				{
					case TEasingType.Linear:
						{
							@this.StartCoroutine(RotationToXLinearIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.QuadIn:
						{
							@this.StartCoroutine(RotationToXQuadInIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.QuadOut:
						{
							@this.StartCoroutine(RotationToXQuadOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.QuadInOut:
						{
							@this.StartCoroutine(RotationToXQuadInOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.CubeIn:
						{
							@this.StartCoroutine(RotationToXCubeInIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.CubeOut:
						{
							@this.StartCoroutine(RotationToXCubeOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.CubeInOut:
						{
							@this.StartCoroutine(RotationToXCubeInOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.BackIn:
						{
							@this.StartCoroutine(RotationToXBackInIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.BackOut:
						{
							@this.StartCoroutine(RotationToXBackOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.BackInOut:
						{
							@this.StartCoroutine(RotationToXBackInOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.ExpoIn:
						{
							@this.StartCoroutine(RotationToXExpoInIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.ExpoOut:
						{
							@this.StartCoroutine(RotationToXExpoOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.ExpoInOut:
						{
							@this.StartCoroutine(RotationToXExpoInOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.SineIn:
						{
							@this.StartCoroutine(RotationToXSineInIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.SineOut:
						{
							@this.StartCoroutine(RotationToXSineOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.SineInOut:
						{
							@this.StartCoroutine(RotationToXSineInOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.ElasticIn:
						{
							@this.StartCoroutine(RotationToXElasticInIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.ElasticOut:
						{
							@this.StartCoroutine(RotationToXElasticOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.ElasticInOut:
						{
							@this.StartCoroutine(RotationToXElasticInOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.BounceIn:
						{
							@this.StartCoroutine(RotationToXBounceInIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.BounceOut:
						{
							@this.StartCoroutine(RotationToXBounceOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.BounceInOut:
						{
							@this.StartCoroutine(RotationToXBounceInOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					default:
						break;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXLinearIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.Linear(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXLinearIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.Linear(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXQuadInIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.QuadIn(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXQuadInIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.QuadIn(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXQuadOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.QuadOut(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXQuadOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.QuadOut(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXQuadInOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.QuadInOut(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXQuadInOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.QuadInOut(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXCubeInIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.CubeIn(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXCubeInIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.CubeIn(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXCubeOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.CubeOut(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXCubeOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.CubeOut(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXCubeInOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.CubeInOut(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXCubeInOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.CubeInOut(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXBackInIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.BackIn(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXBackInIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.BackIn(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXBackOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.BackOut(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXBackOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.BackOut(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXBackInOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.BackInOut(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXBackInOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.BackInOut(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXExpoInIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.ExpoIn(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXExpoInIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.ExpoIn(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXExpoOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.ExpoOut(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXExpoOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.ExpoOut(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXExpoInOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.ExpoInOut(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXExpoInOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.ExpoInOut(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXSineInIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.SineIn(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXSineInIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.SineIn(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXSineOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.SineOut(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXSineOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.SineOut(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXSineInOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.SineInOut(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXSineInOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.SineInOut(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXElasticInIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.ElasticIn(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXElasticInIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.ElasticIn(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXElasticOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.ElasticOut(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXElasticOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.ElasticOut(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXElasticInOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.ElasticInOut(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXElasticInOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.ElasticInOut(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXBounceInIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.BounceIn(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXBounceInIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.BounceIn(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXBounceOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.BounceOut(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXBounceOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.BounceOut(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXBounceInOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.BounceInOut(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси X
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToXBounceInOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.x;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(XEasing.BounceInOut(start_angle, target_angle, time), @this.eulerAngles.y, @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(target_angle, @this.eulerAngles.y, @this.eulerAngles.z);

				on_completed();
			}
			#endregion

			#region ======================================= МЕТОДЫ RotationY ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Старт вращению компонента трансформации к указанному углу по оси Y
			/// </summary>
			/// <param name="this">Пользовательский компонент</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="easing_type">Тип функции скорости</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RotationToY(this MonoBehaviour @this, Single duration, Single target_angle, TEasingType easing_type)
			{
				RotationToY(@this, @this.transform, duration, target_angle, easing_type);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Старт вращению компонента трансформации к указанному углу по оси Y
			/// </summary>
			/// <param name="this">Пользовательский компонент</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="easing_type">Тип функции скорости</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RotationToY(this MonoBehaviour @this, Single duration, Single target_angle, TEasingType easing_type, Action on_completed)
			{
				RotationToY(@this, @this.transform, duration, target_angle, easing_type, on_completed);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Старт вращению компонента трансформации к указанному углу по оси Y
			/// </summary>
			/// <param name="this">Пользовательский компонент</param>
			/// <param name="transform">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="easing_type">Тип функции скорости</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RotationToY(this MonoBehaviour @this, Transform transform, Single duration, Single target_angle, TEasingType easing_type)
			{
				switch (easing_type)
				{
					case TEasingType.Linear:
						{
							@this.StartCoroutine(RotationToYLinearIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.QuadIn:
						{
							@this.StartCoroutine(RotationToYQuadInIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.QuadOut:
						{
							@this.StartCoroutine(RotationToYQuadOutIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.QuadInOut:
						{
							@this.StartCoroutine(RotationToYQuadInOutIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.CubeIn:
						{
							@this.StartCoroutine(RotationToYCubeInIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.CubeOut:
						{
							@this.StartCoroutine(RotationToYCubeOutIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.CubeInOut:
						{
							@this.StartCoroutine(RotationToYCubeInOutIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.BackIn:
						{
							@this.StartCoroutine(RotationToYBackInIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.BackOut:
						{
							@this.StartCoroutine(RotationToYBackOutIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.BackInOut:
						{
							@this.StartCoroutine(RotationToYBackInOutIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.ExpoIn:
						{
							@this.StartCoroutine(RotationToYExpoInIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.ExpoOut:
						{
							@this.StartCoroutine(RotationToYExpoOutIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.ExpoInOut:
						{
							@this.StartCoroutine(RotationToYExpoInOutIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.SineIn:
						{
							@this.StartCoroutine(RotationToYSineInIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.SineOut:
						{
							@this.StartCoroutine(RotationToYSineOutIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.SineInOut:
						{
							@this.StartCoroutine(RotationToYSineInOutIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.ElasticIn:
						{
							@this.StartCoroutine(RotationToYElasticInIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.ElasticOut:
						{
							@this.StartCoroutine(RotationToYElasticOutIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.ElasticInOut:
						{
							@this.StartCoroutine(RotationToYElasticInOutIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.BounceIn:
						{
							@this.StartCoroutine(RotationToYBounceInIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.BounceOut:
						{
							@this.StartCoroutine(RotationToYBounceOutIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.BounceInOut:
						{
							@this.StartCoroutine(RotationToYBounceInOutIteration(transform, duration, target_angle));
						}
						break;
					default:
						break;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Старт вращению компонента трансформации к указанному углу по оси Y
			/// </summary>
			/// <param name="this">Пользовательский компонент</param>
			/// <param name="transform">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="easing_type">Тип функции скорости</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RotationToY(this MonoBehaviour @this, Transform transform, Single duration, Single target_angle, TEasingType easing_type, Action on_completed)
			{
				switch (easing_type)
				{
					case TEasingType.Linear:
						{
							@this.StartCoroutine(RotationToYLinearIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.QuadIn:
						{
							@this.StartCoroutine(RotationToYQuadInIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.QuadOut:
						{
							@this.StartCoroutine(RotationToYQuadOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.QuadInOut:
						{
							@this.StartCoroutine(RotationToYQuadInOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.CubeIn:
						{
							@this.StartCoroutine(RotationToYCubeInIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.CubeOut:
						{
							@this.StartCoroutine(RotationToYCubeOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.CubeInOut:
						{
							@this.StartCoroutine(RotationToYCubeInOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.BackIn:
						{
							@this.StartCoroutine(RotationToYBackInIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.BackOut:
						{
							@this.StartCoroutine(RotationToYBackOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.BackInOut:
						{
							@this.StartCoroutine(RotationToYBackInOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.ExpoIn:
						{
							@this.StartCoroutine(RotationToYExpoInIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.ExpoOut:
						{
							@this.StartCoroutine(RotationToYExpoOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.ExpoInOut:
						{
							@this.StartCoroutine(RotationToYExpoInOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.SineIn:
						{
							@this.StartCoroutine(RotationToYSineInIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.SineOut:
						{
							@this.StartCoroutine(RotationToYSineOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.SineInOut:
						{
							@this.StartCoroutine(RotationToYSineInOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.ElasticIn:
						{
							@this.StartCoroutine(RotationToYElasticInIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.ElasticOut:
						{
							@this.StartCoroutine(RotationToYElasticOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.ElasticInOut:
						{
							@this.StartCoroutine(RotationToYElasticInOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.BounceIn:
						{
							@this.StartCoroutine(RotationToYBounceInIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.BounceOut:
						{
							@this.StartCoroutine(RotationToYBounceOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.BounceInOut:
						{
							@this.StartCoroutine(RotationToYBounceInOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					default:
						break;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYLinearIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.Linear(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYLinearIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.Linear(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYQuadInIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.QuadIn(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYQuadInIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.QuadIn(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYQuadOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.QuadOut(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYQuadOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.QuadOut(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYQuadInOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.QuadInOut(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYQuadInOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.QuadInOut(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYCubeInIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.CubeIn(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYCubeInIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.CubeIn(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYCubeOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.CubeOut(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYCubeOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.CubeOut(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYCubeInOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.CubeInOut(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYCubeInOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.CubeInOut(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYBackInIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.BackIn(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYBackInIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.BackIn(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYBackOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.BackOut(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYBackOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.BackOut(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYBackInOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.BackInOut(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYBackInOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.BackInOut(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYExpoInIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.ExpoIn(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYExpoInIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.ExpoIn(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYExpoOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.ExpoOut(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYExpoOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.ExpoOut(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYExpoInOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.ExpoInOut(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYExpoInOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.ExpoInOut(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYSineInIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.SineIn(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYSineInIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.SineIn(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYSineOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.SineOut(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYSineOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.SineOut(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYSineInOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.SineInOut(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYSineInOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.SineInOut(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYElasticInIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.ElasticIn(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYElasticInIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.ElasticIn(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYElasticOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.ElasticOut(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYElasticOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.ElasticOut(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYElasticInOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.ElasticInOut(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYElasticInOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.ElasticInOut(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYBounceInIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.BounceIn(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYBounceInIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.BounceIn(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYBounceOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.BounceOut(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYBounceOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.BounceOut(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYBounceInOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.BounceInOut(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Y
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToYBounceInOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.y;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, XEasing.BounceInOut(start_angle, target_angle, time), @this.eulerAngles.z);
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, target_angle, @this.eulerAngles.z);

				on_completed();
			}
			#endregion

			#region ======================================= МЕТОДЫ RotationZ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Старт вращению компонента трансформации к указанному углу по оси Z
			/// </summary>
			/// <param name="this">Пользовательский компонент</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="easing_type">Тип функции скорости</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RotationToZ(this MonoBehaviour @this, Single duration, Single target_angle, TEasingType easing_type)
			{
				RotationToZ(@this, @this.transform, duration, target_angle, easing_type);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Старт вращению компонента трансформации к указанному углу по оси Z
			/// </summary>
			/// <param name="this">Пользовательский компонент</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="easing_type">Тип функции скорости</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RotationToZ(this MonoBehaviour @this, Single duration, Single target_angle, TEasingType easing_type, Action on_completed)
			{
				RotationToZ(@this, @this.transform, duration, target_angle, easing_type, on_completed);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Старт вращению компонента трансформации к указанному углу по оси Z
			/// </summary>
			/// <param name="this">Пользовательский компонент</param>
			/// <param name="transform">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="easing_type">Тип функции скорости</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RotationToZ(this MonoBehaviour @this, Transform transform, Single duration, Single target_angle, TEasingType easing_type)
			{
				switch (easing_type)
				{
					case TEasingType.Linear:
						{
							@this.StartCoroutine(RotationToZLinearIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.QuadIn:
						{
							@this.StartCoroutine(RotationToZQuadInIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.QuadOut:
						{
							@this.StartCoroutine(RotationToZQuadOutIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.QuadInOut:
						{
							@this.StartCoroutine(RotationToZQuadInOutIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.CubeIn:
						{
							@this.StartCoroutine(RotationToZCubeInIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.CubeOut:
						{
							@this.StartCoroutine(RotationToZCubeOutIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.CubeInOut:
						{
							@this.StartCoroutine(RotationToZCubeInOutIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.BackIn:
						{
							@this.StartCoroutine(RotationToZBackInIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.BackOut:
						{
							@this.StartCoroutine(RotationToZBackOutIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.BackInOut:
						{
							@this.StartCoroutine(RotationToZBackInOutIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.ExpoIn:
						{
							@this.StartCoroutine(RotationToZExpoInIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.ExpoOut:
						{
							@this.StartCoroutine(RotationToZExpoOutIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.ExpoInOut:
						{
							@this.StartCoroutine(RotationToZExpoInOutIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.SineIn:
						{
							@this.StartCoroutine(RotationToZSineInIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.SineOut:
						{
							@this.StartCoroutine(RotationToZSineOutIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.SineInOut:
						{
							@this.StartCoroutine(RotationToZSineInOutIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.ElasticIn:
						{
							@this.StartCoroutine(RotationToZElasticInIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.ElasticOut:
						{
							@this.StartCoroutine(RotationToZElasticOutIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.ElasticInOut:
						{
							@this.StartCoroutine(RotationToZElasticInOutIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.BounceIn:
						{
							@this.StartCoroutine(RotationToZBounceInIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.BounceOut:
						{
							@this.StartCoroutine(RotationToZBounceOutIteration(transform, duration, target_angle));
						}
						break;
					case TEasingType.BounceInOut:
						{
							@this.StartCoroutine(RotationToZBounceInOutIteration(transform, duration, target_angle));
						}
						break;
					default:
						break;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Старт вращению компонента трансформации к указанному углу по оси Z
			/// </summary>
			/// <param name="this">Пользовательский компонент</param>
			/// <param name="transform">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="easing_type">Тип функции скорости</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			//---------------------------------------------------------------------------------------------------------
			public static void RotationToZ(this MonoBehaviour @this, Transform transform, Single duration, Single target_angle, TEasingType easing_type, Action on_completed)
			{
				switch (easing_type)
				{
					case TEasingType.Linear:
						{
							@this.StartCoroutine(RotationToZLinearIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.QuadIn:
						{
							@this.StartCoroutine(RotationToZQuadInIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.QuadOut:
						{
							@this.StartCoroutine(RotationToZQuadOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.QuadInOut:
						{
							@this.StartCoroutine(RotationToZQuadInOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.CubeIn:
						{
							@this.StartCoroutine(RotationToZCubeInIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.CubeOut:
						{
							@this.StartCoroutine(RotationToZCubeOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.CubeInOut:
						{
							@this.StartCoroutine(RotationToZCubeInOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.BackIn:
						{
							@this.StartCoroutine(RotationToZBackInIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.BackOut:
						{
							@this.StartCoroutine(RotationToZBackOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.BackInOut:
						{
							@this.StartCoroutine(RotationToZBackInOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.ExpoIn:
						{
							@this.StartCoroutine(RotationToZExpoInIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.ExpoOut:
						{
							@this.StartCoroutine(RotationToZExpoOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.ExpoInOut:
						{
							@this.StartCoroutine(RotationToZExpoInOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.SineIn:
						{
							@this.StartCoroutine(RotationToZSineInIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.SineOut:
						{
							@this.StartCoroutine(RotationToZSineOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.SineInOut:
						{
							@this.StartCoroutine(RotationToZSineInOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.ElasticIn:
						{
							@this.StartCoroutine(RotationToZElasticInIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.ElasticOut:
						{
							@this.StartCoroutine(RotationToZElasticOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.ElasticInOut:
						{
							@this.StartCoroutine(RotationToZElasticInOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.BounceIn:
						{
							@this.StartCoroutine(RotationToZBounceInIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.BounceOut:
						{
							@this.StartCoroutine(RotationToZBounceOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					case TEasingType.BounceInOut:
						{
							@this.StartCoroutine(RotationToZBounceInOutIteration(transform, duration, target_angle, on_completed));
						}
						break;
					default:
						break;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZLinearIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.Linear(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZLinearIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.Linear(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZQuadInIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.QuadIn(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZQuadInIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.QuadIn(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZQuadOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.QuadOut(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZQuadOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.QuadOut(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZQuadInOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.QuadInOut(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZQuadInOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.QuadInOut(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZCubeInIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.CubeIn(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZCubeInIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.CubeIn(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZCubeOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.CubeOut(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZCubeOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.CubeOut(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZCubeInOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.CubeInOut(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZCubeInOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.CubeInOut(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZBackInIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.BackIn(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZBackInIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.BackIn(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZBackOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.BackOut(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZBackOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.BackOut(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZBackInOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.BackInOut(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZBackInOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.BackInOut(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZExpoInIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.ExpoIn(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZExpoInIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.ExpoIn(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZExpoOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.ExpoOut(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZExpoOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.ExpoOut(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZExpoInOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.ExpoInOut(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZExpoInOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.ExpoInOut(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZSineInIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.SineIn(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZSineInIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.SineIn(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZSineOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.SineOut(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZSineOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.SineOut(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZSineInOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.SineInOut(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZSineInOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.SineInOut(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZElasticInIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.ElasticIn(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZElasticInIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.ElasticIn(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZElasticOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.ElasticOut(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZElasticOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.ElasticOut(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZElasticInOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.ElasticInOut(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZElasticInOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.ElasticInOut(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZBounceInIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.BounceIn(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZBounceInIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.BounceIn(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZBounceOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.BounceOut(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZBounceOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.BounceOut(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);

				on_completed();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZBounceInOutIteration(this Transform @this, Single duration, Single target_angle)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.BounceInOut(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Подпрограмма вращения компонента трансформации по оси Z
			/// </summary>
			/// <param name="this">Компонент трансформации</param>
			/// <param name="duration">Время вращения</param>
			/// <param name="target_angle">Целевой угол</param>
			/// <param name="on_completed">Обработчик события окончания вращения</param>
			/// <returns>Перечислитель</returns>
			//---------------------------------------------------------------------------------------------------------
			public static IEnumerator RotationToZBounceInOutIteration(this Transform @this, Single duration, Single target_angle, Action on_completed)
			{
				Single time = 0;
				Single start_time = 0;
				Single start_angle = @this.eulerAngles.z;
				while (time < 1)
				{
					start_time += Time.unscaledDeltaTime;
					time = start_time / duration;
					@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, XEasing.BounceInOut(start_angle, target_angle, time));
					yield return null;
				}

				@this.eulerAngles = new Vector3(@this.eulerAngles.x, @this.eulerAngles.y, target_angle);

				on_completed();
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================