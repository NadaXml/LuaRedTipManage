using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using System;

public class RedTipViewElem : GraphView
{
    public new class UxmlFactory : UxmlFactory<RedTipViewElem, GraphView.UxmlTraits> { };
    public RedTipViewElem()
    {
        Insert(0, new GridBackground());

        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/ThirdParty/RedTipGView/Editor/RedTipGView.uss");
        styleSheets.Add(styleSheet);
    }

    internal void PopulateView()
    {
        DeleteElements(graphElements.ToList());
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        //base.BuildContextualMenu(evt);
    }

    public void CreateNode()
    {
        RedTipViewElemNode node = new RedTipViewElemNode();
        AddElement(node);
    }
}
