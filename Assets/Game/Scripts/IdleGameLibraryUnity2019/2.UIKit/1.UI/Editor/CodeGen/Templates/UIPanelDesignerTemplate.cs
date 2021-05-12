/****************************************************************************
 * Copyright (c) 2019.1 .

 ****************************************************************************/

using System.CodeDom;
using QFramework.GraphDesigner;

namespace QFramework
{
    [TemplateClass(TemplateLocation.DesignerFile)]
    [RequiresNamespace("UnityEngine")]
    [RequiresNamespace("UnityEngine.UI")]
    [AsPartial]
    public class UIPanelDesignerTemplate : IClassTemplate<PanelCodeData>,ITemplateCustomFilename
    {
        public string OutputPath  { get; private set; }
        public bool   CanGenerate
        {
            get { return true; }
        }

        public void TemplateSetup()
        {
            Ctx.CurrentDeclaration.BaseTypes.Clear();
            Ctx.CurrentDeclaration.Name = Ctx.Data.PanelName;

            var dataTypeName = Ctx.Data.PanelName + "Data";

            var nameField = new CodeMemberField(typeof(string), "NAME")
            {
                Attributes = MemberAttributes.Const | MemberAttributes.Public,
                InitExpression = Ctx.Data.PanelName.ToCodeSnippetExpression()
            };
            Ctx.CurrentDeclaration.Members.Add(nameField);

            Ctx.Data.MarkedObjInfos.ForEach(info =>
            {
                var field = Ctx.CurrentDeclaration._public_(info.MarkObj.ComponentName, info.Name);

                if (info.MarkObj.Comment.IsNotNullAndEmpty())
                {
                    field.Comments.Add(new CodeCommentStatement(info.MarkObj.Comment));
                }

                field.CustomAttributes.Add(new CodeAttributeDeclaration("SerializeField".ToCodeReference()));
            });

            var data = Ctx.CurrentDeclaration._private_(dataTypeName, "mPrivateData");
            data.InitExpression = new CodeSnippetExpression("null");
        }

        [GenerateMethod, AsOverride]
        protected void ClearUIComponents()
        {
            Ctx.Data.MarkedObjInfos.ForEach(info => { Ctx._("{0} = null", info.Name); });
            Ctx._("mData = null");
        }

        [GenerateProperty]
        public string mData
        {
            get
            {
                var dataTypeName = Ctx.Data.PanelName + "Data";
                Ctx._("return mPrivateData ?? (mPrivateData = new {0}())",dataTypeName);
                Ctx.CurrentProperty.Type = dataTypeName.ToCodeReference();
                return dataTypeName;
            }
            set
            {
                Ctx._("mUIData = value");
                Ctx._("mPrivateData = value");
            }
        }
        public TemplateContext<PanelCodeData> Ctx { get; set; }
        
        public string Filename { get; private set; }
    }
}