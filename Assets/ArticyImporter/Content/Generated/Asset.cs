//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Articy.Unity;
using Articy.Unity.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Articy.Villageofthenecrofarmer
{
    
    
    public class Asset : ArticyObject, IAsset, IPropertyProvider, IObjectWithColor, IObjectWithDisplayName, IObjectWithPreviewImage, IObjectWithText, IObjectWithExternalId, IObjectWithShortId, IObjectWithPosition, IObjectWithZIndex, IObjectWithSize
    {
        
        [SerializeField()]
        private ArticyValueArticyString mDisplayName = new ArticyValueArticyString();
        
        [SerializeField()]
        private ArticyValueArticyString mFilename = new ArticyValueArticyString();
        
        [SerializeField()]
        private ArticyValueArticyString mOriginalSource = new ArticyValueArticyString();
        
        [SerializeField()]
        private PreviewImage mPreviewImage = new PreviewImage();
        
        [SerializeField()]
        private Color mColor;
        
        [SerializeField()]
        private ArticyValueArticyString mText = new ArticyValueArticyString();
        
        [SerializeField()]
        private String mExternalId;
        
        [SerializeField()]
        private Vector2 mPosition;
        
        [SerializeField()]
        private Single mZIndex;
        
        [SerializeField()]
        private Vector2 mSize;
        
        [SerializeField()]
        private UInt32 mShortId;
        
        [SerializeField()]
        private String mAssetRefPath;
        
        [SerializeField()]
        private UnityEngine.Object mCachedAsset;
        
        [SerializeField()]
        private AssetCategory mCategory;
        
        public ArticyString DisplayName
        {
            get
            {
                return mDisplayName.GetValue();
            }
            set
            {
                mDisplayName.SetValue(value);
            }
        }
        
        public ArticyString Filename
        {
            get
            {
                return mFilename.GetValue();
            }
            set
            {
                mFilename.SetValue(value);
            }
        }
        
        public ArticyString OriginalSource
        {
            get
            {
                return mOriginalSource.GetValue();
            }
            set
            {
                mOriginalSource.SetValue(value);
            }
        }
        
        public PreviewImage PreviewImage
        {
            get
            {
                return mPreviewImage;
            }
            set
            {
                var oldValue = mPreviewImage;
                mPreviewImage = value;
                Articy.Unity.ArticyDatabase.ObjectNotifications.ReportChanged(Id, InstanceId, "PreviewImage", oldValue, mPreviewImage);
            }
        }
        
        public Color Color
        {
            get
            {
                return mColor;
            }
            set
            {
                var oldValue = mColor;
                mColor = value;
                Articy.Unity.ArticyDatabase.ObjectNotifications.ReportChanged(Id, InstanceId, "Color", oldValue, mColor);
            }
        }
        
        public ArticyString Text
        {
            get
            {
                return mText.GetValue();
            }
            set
            {
                mText.SetValue(value);
            }
        }
        
        public String ExternalId
        {
            get
            {
                return mExternalId;
            }
            set
            {
                var oldValue = mExternalId;
                mExternalId = value;
                Articy.Unity.ArticyDatabase.ObjectNotifications.ReportChanged(Id, InstanceId, "ExternalId", oldValue, mExternalId);
            }
        }
        
        public Vector2 Position
        {
            get
            {
                return mPosition;
            }
            set
            {
                var oldValue = mPosition;
                mPosition = value;
                Articy.Unity.ArticyDatabase.ObjectNotifications.ReportChanged(Id, InstanceId, "Position", oldValue, mPosition);
            }
        }
        
        public Single ZIndex
        {
            get
            {
                return mZIndex;
            }
            set
            {
                var oldValue = mZIndex;
                mZIndex = value;
                Articy.Unity.ArticyDatabase.ObjectNotifications.ReportChanged(Id, InstanceId, "ZIndex", oldValue, mZIndex);
            }
        }
        
        public Vector2 Size
        {
            get
            {
                return mSize;
            }
            set
            {
                var oldValue = mSize;
                mSize = value;
                Articy.Unity.ArticyDatabase.ObjectNotifications.ReportChanged(Id, InstanceId, "Size", oldValue, mSize);
            }
        }
        
        public UInt32 ShortId
        {
            get
            {
                return mShortId;
            }
            set
            {
                var oldValue = mShortId;
                mShortId = value;
                Articy.Unity.ArticyDatabase.ObjectNotifications.ReportChanged(Id, InstanceId, "ShortId", oldValue, mShortId);
            }
        }
        
        public String AssetRefPath
        {
            get
            {
                return mAssetRefPath;
            }
        }
        
        public AssetCategory Category
        {
            get
            {
                return mCategory;
            }
        }
        
        public TAsset LoadAsset<TAsset>()
            where TAsset : UnityEngine.Object
        {
            if ((mCachedAsset == null))
            {
                mCachedAsset = UnityEngine.Resources.Load(mAssetRefPath);
            }
            return ((TAsset)(mCachedAsset));
        }
        
        public UnityEngine.Sprite LoadAssetAsSprite()
        {
            UnityEngine.Sprite assetSprite = UnityEngine.Resources.Load<UnityEngine.Sprite>(mAssetRefPath);
            if ((assetSprite != null))
            {
                return assetSprite;
            }
            else
            {
                UnityEngine.Texture2D assetTexture = LoadAsset<UnityEngine.Texture2D>();
                if ((assetTexture != null))
                {
                    UnityEngine.Rect spriteRect = new UnityEngine.Rect(0, 0, assetTexture.width, assetTexture.height);
                    return UnityEngine.Sprite.Create(assetTexture, spriteRect, UnityEngine.Vector2.zero);
                }
                return null;
            }
        }
        
        public void ReleaseAsset()
        {
            mCachedAsset = null;
        }
        
        protected override void CloneProperties(object aClone, Articy.Unity.ArticyObject aFirstClassParent)
        {
            Asset newClone = ((Asset)(aClone));
            if ((mDisplayName != null))
            {
                newClone.mDisplayName = ((ArticyValueArticyString)(mDisplayName.CloneObject(newClone, aFirstClassParent)));
            }
            if ((mFilename != null))
            {
                newClone.mFilename = ((ArticyValueArticyString)(mFilename.CloneObject(newClone, aFirstClassParent)));
            }
            if ((mOriginalSource != null))
            {
                newClone.mOriginalSource = ((ArticyValueArticyString)(mOriginalSource.CloneObject(newClone, aFirstClassParent)));
            }
            newClone.PreviewImage = PreviewImage;
            newClone.Color = Color;
            if ((mText != null))
            {
                newClone.mText = ((ArticyValueArticyString)(mText.CloneObject(newClone, aFirstClassParent)));
            }
            newClone.ExternalId = ExternalId;
            newClone.Position = Position;
            newClone.ZIndex = ZIndex;
            newClone.Size = Size;
            newClone.ShortId = ShortId;
            newClone.mAssetRefPath = mAssetRefPath;
            newClone.mCategory = mCategory;
            base.CloneProperties(newClone, aFirstClassParent);
        }
        
        #region property provider interface
        public override void setProp(string aProperty, object aValue)
        {
            if ((aProperty == "DisplayName"))
            {
                DisplayName = System.Convert.ToString(aValue);
                return;
            }
            if ((aProperty == "Filename"))
            {
                Filename = System.Convert.ToString(aValue);
                return;
            }
            if ((aProperty == "OriginalSource"))
            {
                OriginalSource = System.Convert.ToString(aValue);
                return;
            }
            if ((aProperty == "PreviewImage"))
            {
                PreviewImage = ((PreviewImage)(aValue));
                return;
            }
            if ((aProperty == "Color"))
            {
                Color = ((Color)(aValue));
                return;
            }
            if ((aProperty == "Text"))
            {
                Text = System.Convert.ToString(aValue);
                return;
            }
            if ((aProperty == "ExternalId"))
            {
                ExternalId = System.Convert.ToString(aValue);
                return;
            }
            if ((aProperty == "Position"))
            {
                Position = ((Vector2)(aValue));
                return;
            }
            if ((aProperty == "ZIndex"))
            {
                ZIndex = System.Convert.ToSingle(aValue);
                return;
            }
            if ((aProperty == "Size"))
            {
                Size = ((Vector2)(aValue));
                return;
            }
            if ((aProperty == "ShortId"))
            {
                ShortId = ((UInt32)(aValue));
                return;
            }
            base.setProp(aProperty, aValue);
        }
        
        public override Articy.Unity.Interfaces.ScriptDataProxy getProp(string aProperty)
        {
            if ((aProperty == "DisplayName"))
            {
                return new Articy.Unity.Interfaces.ScriptDataProxy(DisplayName);
            }
            if ((aProperty == "Filename"))
            {
                return new Articy.Unity.Interfaces.ScriptDataProxy(Filename);
            }
            if ((aProperty == "OriginalSource"))
            {
                return new Articy.Unity.Interfaces.ScriptDataProxy(OriginalSource);
            }
            if ((aProperty == "PreviewImage"))
            {
                return new Articy.Unity.Interfaces.ScriptDataProxy(PreviewImage);
            }
            if ((aProperty == "Color"))
            {
                return new Articy.Unity.Interfaces.ScriptDataProxy(Color);
            }
            if ((aProperty == "Text"))
            {
                return new Articy.Unity.Interfaces.ScriptDataProxy(Text);
            }
            if ((aProperty == "ExternalId"))
            {
                return new Articy.Unity.Interfaces.ScriptDataProxy(ExternalId);
            }
            if ((aProperty == "Position"))
            {
                return new Articy.Unity.Interfaces.ScriptDataProxy(Position);
            }
            if ((aProperty == "ZIndex"))
            {
                return new Articy.Unity.Interfaces.ScriptDataProxy(ZIndex);
            }
            if ((aProperty == "Size"))
            {
                return new Articy.Unity.Interfaces.ScriptDataProxy(Size);
            }
            if ((aProperty == "ShortId"))
            {
                return new Articy.Unity.Interfaces.ScriptDataProxy(ShortId);
            }
            return base.getProp(aProperty);
        }
        #endregion
    }
}
