
namespace EsHeichSample.Client.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using EsHeichSample.Client.Datas;
    using EsHeichSample.Client.Models;

    public class EditableHeroViewModel : HeroViewModel
    {
        #region Properties
        public new string HeroName 
        { 
            get => base.HeroName; 
            set => base.HeroName = value; 
        }
        public new string RealName
        {
            get => base.RealName;
            set => base.RealName = value;
        }
        public new string Comment
        {
            get => base.Comment;
            set => base.Comment = value;
        }
        public new string HeroName_ko
        {
            get => base.HeroName_ko;
            set => base.HeroName_ko = value;
        }
        public new string ImgPath
        {
            get => base.ImgPath;
            set => base.ImgPath = value;
        }
        public new HeroRole Role
        {
            get => base.Role;
            set => base.Role = value;
        }
        public new string SignatureColor
        {
            get => base.SignatureColor;
            set => base.SignatureColor = value;
        }
        #endregion

        public EditableHeroViewModel(Hero hero) 
            : base(hero)
        {

        }

        protected virtual bool IsValid(Hero hero)
        {
            var properties = typeof(Hero).GetProperties();

            foreach(var prop in properties)
            foreach(var att in prop.GetCustomAttributes(false))
            {
                if(att is ValidationAttribute validation)
                {
                    var valid = validation.IsValid(prop.GetValue(hero));
                    if (valid == false)
                        return false;
                }
            }

            return true;
        }

        public Hero ToModel()
        {
            var editedHero = new Hero
            {
                ID = this.original.ID,
                HeroName = this.HeroName,
                RealName = this.RealName,
                Summary = this.Comment,
                HeroImg = this.ImgPath,
                Role = this.Role,
                HeroName_ko = this.HeroName_ko,
                SignatureColor_Hex = this.SignatureColor,
                SuperPower = this.original.SuperPower
            };

            if (IsValid(editedHero))
                return editedHero;
            else throw new ValidationException("invalidate property");
        }
    }
}
