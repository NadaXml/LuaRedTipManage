using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class RedTipGView : EditorWindow
{
    [MenuItem("Window/UIElements/RedTipGView")]
    public static void ShowExample()
    {
        RedTipGView wnd = GetWindow<RedTipGView>();
        wnd.titleContent = new GUIContent("RedTipGView");
    }

    public void OnEnable()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/ThirdParty/RedTipGView/Editor/RedTipGView.uxml");
        visualTree.CloneTree(root);

        // A stylesheet can be added to a VisualElement.
        // The style will be applied to the VisualElement and all of its children.
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/ThirdParty/RedTipGView/Editor/RedTipGView.uss");
        root.styleSheets.Add(styleSheet);

        RedTipViewElem viewElem = root.Q<RedTipViewElem>("RedTipOV");

        {

            Button btn = root.Q<Button>("Create");
            btn.clickable.clicked += () =>
            {
                viewElem.CreateNode();
            };
        }

        {
            Button btn = root.Q<Button>("Remove");
            btn.clickable.clicked += () =>
            {
                viewElem.PopulateView();
            };
        }

    }

    private void OnSelectionChange()
    {
        
    }
}