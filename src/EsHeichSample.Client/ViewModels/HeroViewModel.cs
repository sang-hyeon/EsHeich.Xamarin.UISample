
namespace EsHeichSample.Client.ViewModels
{
    using System;    
    using System.Text;
    using EsHeichSample.Client.Models;

    public class HeroViewModel : ViewModel
    {
        readonly protected Hero original;

        string _heroName;
        string _realName;
        string _imgPath;
        string _signatureColor;
        string _comment;
        string _heroName_ko;
        HeroRole _role;


        public string HeroName_ko
        {
            get => _heroName_ko;
            protected set => SetProperty(ref _heroName_ko, value);
        }
        public string Comment
        {
            get => _comment;
            protected set => SetProperty(ref _comment, value);
        }   
        public HeroRole Role
        {
            get => _role;
            protected set => SetProperty(ref _role, value);
        }
        public string RealName
        {
            get => _realName;
            protected set => SetProperty(ref _realName, value);
        }
        public string HeroName
        {
            get => _heroName;
            protected set => SetProperty(ref _heroName, value);
        }
        public string ImgPath
        {
            get => _imgPath;
            protected set => SetProperty(ref _imgPath, value);
        }
        public string SignatureColor
        {
            get => _signatureColor;
            protected set => SetProperty(ref _signatureColor, value);
        }

        public HeroViewModel(Hero hero)
        {
            this.original = hero;

            ProjectModel();
        }

        protected void ProjectModel()
        {
            this.HeroName = this.original.HeroName;
            this.RealName = this.original.RealName;
            this.ImgPath = this.original.HeroImg;
            this.SignatureColor = this.original.SignatureColor_Hex;
            this.Role = this.original.Role;
            this.Comment = this.original.Summary;
            this.HeroName_ko = this.original.HeroName_ko;
        }
    }
}
