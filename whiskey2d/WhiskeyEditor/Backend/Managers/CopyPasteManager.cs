using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiskey2D.Core.Managers;
using Whiskey2D.Core.Managers.Impl;


namespace WhiskeyEditor.Backend.Managers
{
    class CopyPasteManager
    {

        private static CopyPasteManager instance = new CopyPasteManager();
        public static CopyPasteManager Instance { get { return instance; } }
        private CopyPasteManager()
        {
            objects = new DefaultObjectManager();
            objects.init();
        }

        private InstanceDescriptor instanceBuffer;
        private ObjectManager objects;


        public void copyToBuffer(InstanceDescriptor inst)
        {
            //instanceBuffer = inst;
            if (inst != null)
            {
                instanceBuffer = inst.clone(objects);
                objects.updateAll();
            }
        }
        public InstanceDescriptor pasteFromBuffer(ObjectManager currentObjects)
        {
            if (instanceBuffer != null)
            {
                InstanceDescriptor clone = instanceBuffer.clone(currentObjects);
                return clone;
            }
            else
            {
                return null;
            }
        }
        public void clearBuffer()
        {
            if (instanceBuffer != null)
            {
                objects.removeObject(instanceBuffer);
                objects.updateAll();
            }
            instanceBuffer = null;
        }


    }
}
