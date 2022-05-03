//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusSystemDispatcher.cs
*		Основной системной диспетчер.
*		Основной системной диспетчер обеспечивает централизованное управление (инициализацию и обновления) и механизм 
*	исполнения остальных подсистем. Он является связующим звеном между центральными диспетчерами различных подсистем 
*	и платформой Unity.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Reflection;
using UnityEngine;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreUnity
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Основной системной диспетчер
		/// </summary>
		/// <remarks>
		/// Основной системной диспетчер обеспечивает централизованное управление (инициализацию и обновления) и
		/// механизм исполнения остальных подсистем.
		/// Он является связующим звеном между центральными диспетчерами различных подсистем и платформой Unity
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		[AddComponentMenu("Lotus/Core/System Dispatcher")]
		[LotusExecutionOrder(5)]
		public class LotusSystemDispatcher : LotusSystemSingleton<LotusSystemDispatcher>, ISerializationCallbackReceiver,
			ILotusSingleton, ILotusMessageHandler
		{
			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			/// <summary>
			/// Имя сообщения для показа/скрытия FPS
			/// </summary>
			public const String MESSAGE_FPS = "FPS";

			/// <summary>
			/// Имя сообщения для показа/скрытия дополнительной информации
			/// </summary>
			public const String MESSAGE_INFO = "Info";
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			[SerializeField]
			internal Boolean mIsMainInstance;
			[SerializeField]
			internal TSingletonDestroyMode mDestroyMode;
			[SerializeField]
			internal Boolean mIsDontDestroy;
			[SerializeField]
			internal String mGroupMessage = "System";

			// Параметры разработки IMGUI
			[SerializeField]
			internal Boolean mUseIMGUI;
			[SerializeField]
			internal GUISkin mCurrentSkin;

			// Управление консолью
			[SerializeField]
			internal Boolean mConsoleVisible;
			[SerializeField]
			internal Single mConsoleHeight = 300;

			// Данные по FPS
			[SerializeField]
			internal Boolean mIsFpsShow = true;
			[SerializeField]
			internal Rect mFpsPosition = new Rect(5, 5, 100, 25);
			[SerializeField]
			internal Single mFpsUpdateInterval = 0.5f;
			[NonSerialized]
			internal GUIStyle mFpsGUIStyle;
			[NonSerialized]
			internal Single mFpsMin = 200;
			[NonSerialized]
			internal Single mFpsAccum = 0;
			[NonSerialized]
			internal Int32 mFpsFrames = 0;
			[NonSerialized]
			internal Single mFpsTimeStart;
			[NonSerialized]
			internal String mFpsText = "";
			[NonSerialized]
			internal Action mOnUpdateFps;

			// Дополнительная информация
			[SerializeField]
			internal Boolean mIsInfoShow = false;
			[SerializeField]
			internal Rect mInfoPosition;
			[NonSerialized]
			internal String mInfoText;

			// Данные в режиме редактора
#if UNITY_EDITOR
			[SerializeField]
			internal Int32 mSelectedTab;
#endif
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Статус основного экземпляра
			/// </summary>
			public Boolean IsMainInstance
			{
				get
				{
					return mIsMainInstance;
				}
				set
				{
					mIsMainInstance = value;
				}
			}

			/// <summary>
			/// Статус удаления игрового объекта
			/// </summary>
			/// <remarks>
			/// При дублировании будет удалятся либо непосредственного игровой объект либо только компонент
			/// </remarks>
			public TSingletonDestroyMode DestroyMode
			{
				get
				{
					return mDestroyMode;
				}
				set
				{
					mDestroyMode = value;
				}
			}

			/// <summary>
			/// Не удалять объект когда загружается новая сцена
			/// </summary>
			public Boolean IsDontDestroy
			{
				get
				{
					return mIsDontDestroy;
				}
				set
				{
					mIsDontDestroy = value;
				}
			}

			//
			// ПАРАМЕТРЫ РАЗРАБОТКИ IMGUI
			//
			/// <summary>
			/// Использовать графику технологии IMGUI
			/// </summary>
			public Boolean UseIMGUI
			{
				get
				{
					return mUseIMGUI;
				}
				set
				{
					mUseIMGUI = value;
				}
			}

			/// <summary>
			/// Текущий визуальный скин для рисования
			/// </summary>
			public GUISkin CurrentSkin
			{
				get
				{
					return mCurrentSkin;
				}
				set
				{
					mCurrentSkin = value;
					if(mCurrentSkin != null && XGUISkin.IsDefaultSkinNull)
					{
						XGUISkin.DefaultSkin = mCurrentSkin;
					}
				}
			}

			//
			// КОНСОЛЬ
			//
			/// <summary>
			/// Отображение консоли
			/// </summary>
			public Boolean ConsoleVisible
			{
				get
				{
					return mConsoleVisible;
				}
				set
				{
					mConsoleVisible = value;
				}
			}

			/// <summary>
			/// Высота консоли
			/// </summary>
			public Single ConsoleHeight
			{
				get
				{
					return mConsoleHeight;
				}
				set
				{
					mConsoleHeight = value;
				}
			}

			//
			// ДАННЫЕ ПО FPS
			//
			/// <summary>
			/// Количества кадров в секунду
			/// </summary>
			public Single FPS
			{
				get
				{
					return (mFpsMin);
				}
			}

			/// <summary>
			/// Отображение количества кадров в секунду
			/// </summary>
			public Boolean IsFpsShow
			{
				get
				{
					return mIsFpsShow;
				}
				set
				{
					mIsFpsShow = value;
				}
			}

			/// <summary>
			/// Обновление интервала FPS
			/// </summary>
			public Single FpsUpdateInterval
			{
				get
				{
					return mFpsUpdateInterval;
				}
				set
				{
					mFpsUpdateInterval = value;
				}
			}

			/// <summary>
			/// Позиция для вывода количества кадров в секунду
			/// </summary>
			public Rect FpsPosition
			{
				get
				{
					return mFpsPosition;
				}
				set
				{
					mFpsPosition = value;
				}
			}

			/// <summary>
			/// Обработчик события обновления вывода количества кадров в секунду
			/// </summary>
			public Action OnUpdateFps
			{
				get
				{
					return mOnUpdateFps;
				}
				set
				{
					mOnUpdateFps = value;
				}
			}
			#endregion

			#region ======================================= СВОЙСТВА ILotusMessageHandler =============================
			/// <summary>
			/// Группа которой принадлежит данный обработчик событий
			/// </summary>
			public String MessageHandlerGroup
			{
				get
				{
					return (mGroupMessage);
				}
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
				//
				// БАЗОВОЕ ЯДРО
				//

				// Лог (для дальнейшего логирования)
				XConsoleDispatcher.OnResetEditor();

				// Подсистема ввода
				XInputDispatcher.OnResetEditor();

				// Инициализация центрального диспетчера игровых объектов
				XGameObjectDispatcher.OnResetEditor();

				// Инициализация центрального диспетчера компонентов
				XComponentDispatcher.OnResetEditor();

				// Инициализация центрального диспетчера ресурсов
				XResourcesDispatcher.OnResetEditor();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Псевдоконструктор скрипта
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Awake()
			{
				// FPS
				InitFPS();

				// Подсистема IMGUI
				if (mUseIMGUI)
				{
					InitIMGUI();
				}

				// Инициализация данных
				if (!CheckDublicate())
				{
					//
					// БАЗОВОЕ ЯДРО
					//
					// Лог (для дальнейшего логирования)
					XConsoleDispatcher.OnInit();

					// Подсистема ввода
					XInputDispatcher.OnInit();

					// Инициализация центрального диспетчера игровых объектов
					XGameObjectDispatcher.OnInit();

					// Инициализация центрального диспетчера компонентов
					XComponentDispatcher.OnInit();

					// Инициализация центрального диспетчера ресурсов
					XResourcesDispatcher.OnInit();
				}

				if (mIsDontDestroy)
				{
					GameObject.DontDestroyOnLoad(this.gameObject);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Включение компонента
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void OnEnable()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Старт скрипта
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Start()
			{
				if (!CheckDublicate())
				{
					//
					// БАЗОВЫЙ МОДУЛЬ UNITY
					//
					XInputDispatcher.OnStart();
				}

				mInfoPosition = new Rect(5, 5, 340, 40);
				mInfoPosition.x = LotusSystemSettingsService.Instance.ScreenWidth - mInfoPosition.width - mInfoPosition.x;
				mInfoText = "Screen Width = " + Screen.width.ToString() + "; Screen Height = " + Screen.height.ToString();
				Single sx = LotusSystemSettingsService.Instance.ScaledScreenX;
				Single sy = LotusSystemSettingsService.Instance.ScaledScreenY;
				mInfoText += "\n" + "ScaledScreenX = " + sx.ToString("F2") + "; ScaledScreenY = " + sy.ToString("F2");
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление скрипта каждый кадр
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void Update()
			{
				// Обновляем FPS
				UpdateFPS();

				// Обработка событий ввода
				XInputDispatcher.OnUpdate();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление скрипта каждый кадр после (после Update)
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void LateUpdate()
			{
				XInputDispatcher.OnLateUpdate();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Рисование UnityGUI
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void OnGUI()
			{
				if (mUseIMGUI)
				{
					UpdateIMGUI();
				}

				if (mConsoleVisible)
				{
					XConsoleDispatcher.OnGUI(mConsoleHeight);
				}

				if (mIsFpsShow)
				{
					GUI.backgroundColor = Color.black;
					GUI.contentColor = Color.yellow;
					GUI.Box(mFpsPosition, mFpsText, mFpsGUIStyle);
				}

				if(mIsInfoShow)
				{
					GUI.backgroundColor = Color.black;
					GUI.contentColor = Color.yellow;
					GUI.Box(mInfoPosition, mInfoText, mFpsGUIStyle);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Отключение компонента
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void OnDisable()
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Игровой объект уничтожился
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void OnDestroy()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Завершение работы приложения
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void OnApplicationQuit()
			{
				XConsoleDispatcher.OnShudown();
			}
			#endregion

			#region ======================================= МЕТОДЫ ISerializationCallbackReceiver =====================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Процесс перед сериализацией объекта
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void OnBeforeSerialize()
			{
				if (mCurrentSkin != null && XGUISkin.IsDefaultSkinNull)
				{
					XGUISkin.DefaultSkin = mCurrentSkin;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Процесс после сериализацией объекта
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public void OnAfterDeserialize()
			{
				if (mCurrentSkin != null && XGUISkin.IsDefaultSkinNull)
				{
					XGUISkin.DefaultSkin = mCurrentSkin;
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ ILotusMessageHandler ===============================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Основной метод для обработки сообщения
			/// </summary>
			/// <param name="args">Аргументы сообщения</param>
			/// <returns>Код обработки собщения</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 OnMessageHandler(CMessageArgs args)
			{
				Boolean status = false;
				if (args is CMessageConsole)
				{
					CMessageConsole args_console = args as CMessageConsole;
					if(MESSAGE_FPS.EqualIgnoreCase(args_console.Name))
					{
						mIsFpsShow = XBoolean.Parse(args_console.Argument);
						status = true;
					}
					if (MESSAGE_INFO.EqualIgnoreCase(args_console.Name))
					{
						mIsInfoShow = XBoolean.Parse(args_console.Argument);
						status = true;
					}
				}
				else
				{
					switch (args.Name)
					{
						case MESSAGE_FPS:
							{
								mIsFpsShow = args.BooleanValue;
								status = true;
							}
							break;
						default:
							break;
					}
				}

				return (10);
			}
			#endregion

			#region ======================================= МЕТОДЫ РАБОТЫ С FPS =======================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Инициализация подсистемы FPS
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			private void InitFPS()
			{
				if (mFpsGUIStyle == null)
				{
					mFpsGUIStyle = new GUIStyle(GUIStyle.none);
					mFpsGUIStyle.fontSize = 18;
					mFpsGUIStyle.alignment = TextAnchor.MiddleCenter;
					mFpsGUIStyle.normal.background = XTexture2D.BlackAlpha75;
					mFpsGUIStyle.normal.textColor = Color.white;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление данных о текущем FPS
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			private void UpdateFPS()
			{
				mFpsTimeStart -= Time.unscaledDeltaTime;
				mFpsAccum += 1.0f / Time.unscaledDeltaTime;
				++mFpsFrames;

				// Interval ended - update GUI text and start new interval
				if (mFpsTimeStart <= 0.0)
				{
					Single fps = mFpsAccum / mFpsFrames;
					mFpsTimeStart = mFpsUpdateInterval;
					mFpsAccum = 0.0F;
					mFpsFrames = 0;
					if (mFpsMin > fps)
					{
						mFpsMin = fps;
					}

					mFpsText = "FPS = " + fps.ToString("F2");
					if (mOnUpdateFps != null) mOnUpdateFps();

				}
			}
			#endregion

			#region ======================================= МЕТОДЫ РАБОТЫ С IMGUI =====================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Инициализация подсистемы IMGUI
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			private void InitIMGUI()
			{

			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление подсистемы IMGUI
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			private void UpdateIMGUI()
			{
				GUI.skin = mCurrentSkin;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================