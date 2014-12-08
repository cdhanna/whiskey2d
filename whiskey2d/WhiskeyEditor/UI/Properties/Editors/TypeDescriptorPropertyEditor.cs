﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiskeyEditor.Backend;
using System.Windows.Forms;
using System.Drawing;
using WhiskeyEditor.Backend.Actions;

namespace WhiskeyEditor.UI.Properties.Editors
{
    class TypeDescriptorPropertyEditor : FileDescriptorPropertyEditor<TypeDescriptor>
    {

        //public TypeDescriptor TypeDescriptor { get; private set; }

        private WhiskeyAction addPropertyAction;
        private List<WhiskeyAction> remPropertyActions;
        ToolStripDropDownButton dropDownBtn;



        public TypeDescriptorPropertyEditor(TypeDescriptor tDesc) : base(tDesc)
        {
            
            Title = "Type Properties";

            
            

            initControls();
            addControls();
           
        }


        private void initControls()
        {
            WhiskeyPropertyListGrid.PropertyList = Descriptor.getPropertySet();

            remPropertyActions = new List<WhiskeyAction>();
           
            WhiskeyPropertyListGrid.PropertyGrid.PropertyValueChanged += (s, a) =>
            {
                Descriptor.ensureFileExists();
            };

            addPropertyAction = new AddPropertyAction(Descriptor, WhiskeyPropertyListGrid);
            ToolStripItems.Add(addPropertyAction.generateControl<ToolStripButton>());


            dropDownBtn = new ToolStripDropDownButton("Remove", Assets.AssetManager.ICON_MINUS);


            updateItems();

           // dropDownBtn.DropDownItems.Add(removeBox);
            dropDownBtn.DropDownDirection = ToolStripDropDownDirection.BelowRight;
            dropDownBtn.DropDown.AutoSize = true;
            dropDownBtn.DropDown.Width = 300;
            //removeBox.Alignment = ToolStripItemAlignment.Right;
            //removeBox.ImageIndex = 1;
            //removeBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ToolStripItems.Add(dropDownBtn);


            //Descriptor.PropertyRemoved += (s, a) =>
            //{
            //    WhiskeyPropertyListGrid.Refresh();
            //    updateItems();
            //};

        }

        private void updateItems()
        {
            dropDownBtn.DropDownItems.Clear();
            remPropertyActions.Clear();
            foreach (PropertyDescriptor prop in Descriptor.getPropertySet())
            {
                if (!prop.Secure){
                    RemovePropertyAction rem = new RemovePropertyAction(Descriptor, prop);
                    remPropertyActions.Add(rem);
                    dropDownBtn.DropDownItems.Add(rem.generateControl<ToolStripButton>());
                }
            }
        }

        private void addControls()
        {
            
        }

    }
}