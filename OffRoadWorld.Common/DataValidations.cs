﻿using Microsoft.VisualBasic;

namespace OffRoadWorld.Common
{
    public class DataValidations
    {
        public class Event
        {
            public const int TitleMinLength = 5;
            public const int TitleMaxLength = 25;

            public const int DescriptionMinLength = 25;
            public const int DescriptionMaxLength = 500;

            public const int AddressMinLength = 6;
            public const int AddressMaxLength = 50;
        }

        public class Category
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 15;
        }

        public class Vehicle
        {
            public const int MakeMinLength = 2;
            public const int MakeMaxLength = 20;

            public const int ModelMinLength = 3;
            public const int ModelMaxLength = 10;

            public const int MinProductionYear = 1970;
            public const int MaxProductionYear = 2024;

            public const int UrlMaxLength = 2048;

            public const int MinHorsePower = 1;
            public const int MaxHorsePower = 1000;

            public const int MinEngineCapacity = 49;
            public const int MaxEngineCapacity = 5500;
        }

        public class Forum
        {
            public const int TitleMinLength = 3;
            public const int TitleMaxLenght = 35;

            public const int DescriptionMinLength = 20;
            public const int DescriptionMaxLength = 100;
        }

        public class Topic
        {
            public const int TitleMinLength = 7;
            public const int TitleMaxLength = 25;

            public const int ContentMinLength = 40;
            public const int ContentMaxLength = 2500;
        }

        public class Post
        {
            public const int ContentMinLength = 40;
            public const int ContentMaxLength = 2500;
        }
    }
}