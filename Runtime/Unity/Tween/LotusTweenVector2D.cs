﻿//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема анимации
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusTweenVector2D.cs
*		Компонент для хранения и управления анимацией значением двухмерного вектора.
*		Реализация компонента для хранения (используется хранилища анимационных кривых) и управления анимацией
*	двухмерного вектора.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using UnityEngine;
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Core;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreUnityTween
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Компонент для хранения и управления анимацией значением двухмерного вектора
		/// </summary>
		/// <remarks>
		/// Реализация компонента для хранения (используется хранилища анимационных кривых) и управления анимацией
		/// двухмерного вектора
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		[AddComponentMenu("Lotus/Core/Tween/Animation Vector2D")]
		public class LotusTweenVector2D : MonoBehaviour
		{
			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			[SerializeField]
			[LotusDisplayName(nameof(Tween))]
			[LotusSerializeMember(nameof(Tween))]
			internal CTweenVector2D mTweenVector2D;

			[SerializeField]
			[LotusDisplayName(nameof(UseTransform))]
			[LotusSerializeMember(nameof(UseTransform))]
			internal Boolean mUseTransform;

			[SerializeField]
			[LotusDisplayName(nameof(Parameter))]
			[LotusSerializeMember(nameof(Parameter))]
			internal TTweenTransformParameterType mParameter;

			[SerializeField]
			[LotusDisplayName(nameof(PlaneProjection))]
			[LotusSerializeMember(nameof(PlaneProjection))]
			internal TDimensionPlane mPlaneProjection;

			[NonSerialized]
			internal Transform mThisTransform;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Аниматор значения 2D вектора
			/// </summary>
			public CTweenVector2D Tween
			{
				get { return (mTweenVector2D); }
			}

			/// <summary>
			/// Текущее значение переменной
			/// </summary>
			public Vector2 Value
			{
				get { return mTweenVector2D.mValue; }
			}

			/// <summary>
			/// Начальное значение переменной
			/// </summary>
			public Vector2 StartValue
			{
				get { return mTweenVector2D.mStartValue; }
				set { mTweenVector2D.mStartValue = value; }
			}

			/// <summary>
			/// Целевое значение переменной
			/// </summary>
			public Vector2 TargetValue
			{
				get { return mTweenVector2D.mTargetValue; }
				set { mTweenVector2D.mTargetValue = value; }
			}

			/// <summary>
			/// Режим проигрывания анимации 
			/// </summary>
			public TTweenWrapMode WrapMode
			{
				get { return mTweenVector2D.mWrapMode; }
				set { mTweenVector2D.mWrapMode = value; }
			}

			/// <summary>
			/// Время в течение которого должно измениться значение переменной от начального к конечной
			/// </summary>
			public Single TimeAnimation
			{
				get { return mTweenVector2D.mCorrectTime; }
				set { mTweenVector2D.mCorrectTime = value; }
			}

			/// <summary>
			/// Нормализованное время прохождение анимации в пределах от 0 до 1
			/// </summary>
			public Single NormalizeTime
			{
				get { return mTweenVector2D.mNormalizeTime; }
			}

			/// <summary>
			/// Статус игнорирования масштабирования времени
			/// </summary>
			public Boolean IgnoreTimeScale
			{
				get { return mTweenVector2D.IgnoreTimeScale; }
				set { mTweenVector2D.IgnoreTimeScale = value; }
			}

			/// <summary>
			/// Количество циклов проигрывания циклических анимаций. 0 - бесконечно
			/// </summary>
			public Int32 CountLoop
			{
				get { return mTweenVector2D.mCountLoop; }
				set { mTweenVector2D.mCountLoop = value; }
			}

			/// <summary>
			/// Текущие количество проигрывания циклических анимаций
			/// </summary>
			public Int32 CurrentCountLoop
			{
				get { return mTweenVector2D.mCurrentCountLoop; }
				set { mTweenVector2D.mCurrentCountLoop = value; }
			}

			/// <summary>
			/// Статус применения изменяемого значения к текущему объекту трансформации
			/// </summary>
			public Boolean UseTransform
			{
				get { return mUseTransform; }
				set { mUseTransform = value; }
			}

			/// <summary>
			/// Параметр трансформации к которому применяется изменяемое значение
			/// </summary>
			public TTweenTransformParameterType Parameter
			{
				get { return mParameter; }
				set { mParameter = value; }
			}

			/// <summary>
			/// Плоскость проекции – конкретизация параметра трансформации
			/// </summary>
			public TDimensionPlane PlaneProjection
			{
				get { return mPlaneProjection; }
				set { mPlaneProjection = value; }
			}

			/// <summary>
			/// Статус анимации
			/// </summary>
			public Boolean IsPlay
			{
				get { return mTweenVector2D.mStart; }
			}
			
			/// <summary>
			/// Установка/снятие паузы анимации
			/// </summary>
			public Boolean IsPause
			{
				get { return mTweenVector2D.mIsPause; }
				set { mTweenVector2D.mIsPause = value; }
			}

			/// <summary>
			/// Статус проигрывания анимации вперед
			/// </summary>
			public Boolean IsForward
			{
				get { return mTweenVector2D.mIsForward; }
			}

			/// <summary>
			/// Событие для нотификации о начале анимации. Аргумент - название анимации
			/// </summary>
			public Action<String> OnAnimationStart
			{
				get { return mTweenVector2D.mOnAnimationStart; }
				set { mTweenVector2D.mOnAnimationStart = value; }
			}

			/// <summary>
			/// Событие для нотификации об окончании анимации. Аргумент - название анимации
			/// </summary>
			public Action<String> OnAnimationCompleted
			{
				get { return mTweenVector2D.mOnAnimationCompleted; }
				set { mTweenVector2D.mOnAnimationCompleted = value; }
			}
			#endregion

			#region ======================================= СОБЫТИЯ UNITY =============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Инициализация скрипта при присоединении его к объекту(в режиме редактора)
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Reset()
			{
				Init();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Псевдоконструктор скрипта
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Awake()
			{
				Init();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Включение скрипта
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void OnEnable()
			{
				Init();
			}
			
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Старт скрипта
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Start()
			{
				mThisTransform = this.transform;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление скрипта каждый кадр
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Update()
			{
				if (mTweenVector2D.IsPlay)
				{
					mTweenVector2D.UpdateAnimation();
				}

				if (mUseTransform)
				{
					this.UpdateTransform();
				}
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Первичная инициализация данных и параметров компонента
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Init()
			{
				if (mTweenVector2D == null)
				{
					mTweenVector2D = new CTweenVector2D();
				}
			}
			
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Старт анимации
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void StartAnimation()
			{
				mTweenVector2D.StartAnimation();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Старт анимации
			/// </summary>
			/// <param name="target_position">Целевая позиция</param>
			//---------------------------------------------------------------------------------------------------------
			public void StartAnimation(Vector2 target_position)
			{
				mTweenVector2D.StartValue = new Vector2(mThisTransform.position.x, mThisTransform.position.y);
				mTweenVector2D.TargetValue = target_position;

				mTweenVector2D.StartAnimation();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Остановка анимации
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void StopAnimation()
			{
				mTweenVector2D.StopAnimation();
			}
			
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление трансформации
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void UpdateTransform()
			{
				switch (mParameter)
				{
					case TTweenTransformParameterType.Position:
						{
							switch (mPlaneProjection)
							{
								case TDimensionPlane.XY:
									{
										mThisTransform.position = new Vector3(mTweenVector2D.Value.x, mTweenVector2D.Value.y, mThisTransform.position.z);
									}
									break;
								case TDimensionPlane.XZ:
									{
										mThisTransform.position = new Vector3(mTweenVector2D.Value.x, mThisTransform.position.y, mTweenVector2D.Value.y);
									}
									break;
								case TDimensionPlane.ZY:
									{
										mThisTransform.position = new Vector3(mThisTransform.position.x, mTweenVector2D.Value.x, mTweenVector2D.Value.y);
									}
									break;
								default:
									break;
							}
						}
						break;
					case TTweenTransformParameterType.LocalPosition:
						{
							switch (mPlaneProjection)
							{
								case TDimensionPlane.XY:
									{
										mThisTransform.localPosition = new Vector3(mTweenVector2D.Value.x, mTweenVector2D.Value.y, mThisTransform.localPosition.z);
									}
									break;
								case TDimensionPlane.XZ:
									{
										mThisTransform.localPosition = new Vector3(mTweenVector2D.Value.x, mThisTransform.localPosition.y, mTweenVector2D.Value.y);
									}
									break;
								case TDimensionPlane.ZY:
									{
										mThisTransform.localPosition = new Vector3(mThisTransform.localPosition.x, mTweenVector2D.Value.x, mTweenVector2D.Value.y);
									}
									break;
								default:
									break;
							}
						}
						break;
					case TTweenTransformParameterType.Rotation:
						{
							switch (mPlaneProjection)
							{
								case TDimensionPlane.XY:
									{
										mThisTransform.eulerAngles = new Vector3(mTweenVector2D.Value.x, mTweenVector2D.Value.y,
											mThisTransform.eulerAngles.z);
									}
									break;
								case TDimensionPlane.XZ:
									{
										mThisTransform.eulerAngles = new Vector3(mTweenVector2D.Value.x, mThisTransform.eulerAngles.y,
											mTweenVector2D.Value.y);
									}
									break;
								case TDimensionPlane.ZY:
									{
										mThisTransform.eulerAngles = new Vector3(mThisTransform.eulerAngles.x, mTweenVector2D.Value.x,
											mTweenVector2D.Value.y);
									}
									break;
								default:
									break;
							}
						}
						break;
					case TTweenTransformParameterType.LocalRotation:
						{
							switch (mPlaneProjection)
							{
								case TDimensionPlane.XY:
									{
										mThisTransform.localEulerAngles = new Vector3(mTweenVector2D.Value.x, mTweenVector2D.Value.y,
											mThisTransform.localEulerAngles.z);
									}
									break;
								case TDimensionPlane.XZ:
									{
										mThisTransform.localEulerAngles = new Vector3(mTweenVector2D.Value.x, mThisTransform.localEulerAngles.y,
											mTweenVector2D.Value.y);
									}
									break;
								case TDimensionPlane.ZY:
									{
										mThisTransform.localEulerAngles = new Vector3(mThisTransform.localEulerAngles.x, mTweenVector2D.Value.x,
											mTweenVector2D.Value.y);
									}
									break;
								default:
									break;
							}
						}
						break;
					case TTweenTransformParameterType.Scale:
						{
							switch (mPlaneProjection)
							{
								case TDimensionPlane.XY:
									{
										mThisTransform.localScale = new Vector3(mTweenVector2D.Value.x, mTweenVector2D.Value.y, mThisTransform.localScale.z);
									}
									break;
								case TDimensionPlane.XZ:
									{
										mThisTransform.localScale = new Vector3(mTweenVector2D.Value.x, mThisTransform.localScale.y, mTweenVector2D.Value.y);
									}
									break;
								case TDimensionPlane.ZY:
									{
										mThisTransform.localScale = new Vector3(mThisTransform.localScale.x, mTweenVector2D.Value.x, mTweenVector2D.Value.y);
									}
									break;
								default:
									break;
							}
						}
						break;
					default:
						break;
				}
			}

#if UNITY_EDITOR
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Запуск анимации
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			[ContextMenu("Play")]
			[LotusMethodCall("Play", 0, TMethodCallMode.OnlyPlay)]
			public void PlayInEditor()
			{
				StartAnimation();
			}
#endif
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================