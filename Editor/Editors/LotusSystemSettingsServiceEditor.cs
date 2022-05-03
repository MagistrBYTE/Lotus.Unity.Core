//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusSystemSettingsServiceEditor.cs
*		Редактор сервиса для хранения глобальных системных и пользовательских настроек системы Lotus.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Core;
//=====================================================================================================================
//---------------------------------------------------------------------------------------------------------------------
/// <summary>
/// Редактор сервиса для хранения глобальных системных и пользовательских настроек системы Lotus
/// </summary>
//---------------------------------------------------------------------------------------------------------------------
[CustomEditor(typeof(LotusSystemSettingsService))]
public class LotusSystemSettingsServiceEditor : Editor
{
	#region =============================================== СТАТИЧЕСКИЕ ДАННЫЕ ========================================
	protected const String mDescriptionService = "Сервис для хранения глобальных системных и пользовательских настроек системы Lotus";
	protected static readonly GUIContent mContentDeleteSkin = new GUIContent("X", "Delete reference to skin");
	protected static readonly GUIContent mContentSetSkin = new GUIContent("S", "Set skin to current");
	protected static readonly GUIContent mContentAddSkin = new GUIContent("Add", "Add reference to skin");
	protected static readonly GUIContent mContentFind = new GUIContent("Find resources", "Find and load resources");
	#endregion

	#region =============================================== СТАТИЧЕСКИЕ МЕТОДЫ ========================================
	//-----------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Создания центрального сервиса для управления визуальными стилями и скинами
	/// </summary>
	//-----------------------------------------------------------------------------------------------------------------
	[MenuItem(XCoreEditorSettings.MenuPath + "Create System Settings Service", false, XCoreEditorSettings.MenuOrderLast + 20)]
	public static void Create()
	{
#pragma warning disable 0219
		LotusSystemSettingsService system_settings_service = LotusSystemSettingsService.Instance;
#pragma warning restore 0219
	}
	#endregion

	#region =============================================== ДАННЫЕ ====================================================
	private LotusSystemSettingsService mSystemSettingsService;
	#endregion

	#region =============================================== СОБЫТИЯ UNITY =============================================
	//-----------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Включение скрипта в инспекторе объектов
	/// </summary>
	//-----------------------------------------------------------------------------------------------------------------
	public void OnEnable()
	{
		mSystemSettingsService = this.target as LotusSystemSettingsService;
	}

	//-----------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Рисование в инспекторе объектов
	/// </summary>
	//-----------------------------------------------------------------------------------------------------------------
	public override void OnInspectorGUI()
	{
		GUILayout.Space(4.0f);
		EditorGUILayout.HelpBox(mDescriptionService, MessageType.Info);

		// Параметры разработки
		DrawDesign();

		GUILayout.Space(2.0f);
	}
	#endregion

	#region =============================================== МЕТОДЫ РИСОВАНИЯ ==========================================
	//-----------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Рисование параметров разработки
	/// </summary>
	//-----------------------------------------------------------------------------------------------------------------
	public void DrawDesign()
	{
		XEditorInspector.DrawCaption("Параметры разработки");

		EditorGUI.BeginChangeCheck();

		GUILayout.Space(2.0f);
		mSystemSettingsService.DesignWidth = XEditorInspector.PropertyInt("Design Width", mSystemSettingsService.DesignWidth);

		GUILayout.Space(2.0f);
		mSystemSettingsService.DesignHeight = XEditorInspector.PropertyInt("Design Height", mSystemSettingsService.DesignHeight);

		if (EditorGUI.EndChangeCheck())
		{
			serializedObject.Save();
		}
	}
	#endregion
}
//=====================================================================================================================