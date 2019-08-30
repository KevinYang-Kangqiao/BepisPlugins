﻿using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using AIChara;
using static AIChara.ChaFileDefine;
using CategoryNo = AIChara.ChaListDefine.CategoryNo;


namespace Sideloader.AutoResolver
{
    public struct CategoryProperty
    {
        public CategoryNo Category;
        public string Property;

        public string Prefix;

        public CategoryProperty(CategoryNo category, string property, string prefix = "")
        {
            Category = category;
            Property = property;

            Prefix = prefix;
        }

        public override string ToString() => Prefix != "" ? $"{Prefix}.{Property}" : Property;
    }

    public static class StructReference
    {
        #region Helper Methods
        private static Dictionary<CategoryProperty, StructValue<int>> GeneratePropertyInfoDictionary(Type t, IEnumerable<CategoryProperty> properties, string prefix = "")
        {
            var result = new Dictionary<CategoryProperty, StructValue<int>>();

            foreach (CategoryProperty property in properties)
            {
                var newProp = property;

                if (prefix != "")
                    newProp.Prefix = prefix;

                result.Add(newProp, new StructValue<int>(AccessTools.Property(t, newProp.Property)));
            }

            return result;
        }
        #endregion

        #region ChaFileFace
        private static Dictionary<CategoryProperty, StructValue<int>> _chaFileFaceGenerator()
        {
            const string prefix = nameof(ChaFileFace);

            var baseProperties = new List<CategoryProperty>
            {
                new CategoryProperty(CategoryNo.ft_detail_f, "detailId"),
                new CategoryProperty(CategoryNo.st_eyebrow, "eyebrowId"),
                new CategoryProperty(CategoryNo.st_eyelash, "eyelashesId"),
                new CategoryProperty(CategoryNo.fo_head, "headId"),
                new CategoryProperty(CategoryNo.st_eye_hl, "hlId"),
                new CategoryProperty(CategoryNo.st_mole, "moleId"),
                new CategoryProperty(CategoryNo.ft_skin_f, "skinId")
            };

            var generatedProperties = GeneratePropertyInfoDictionary(typeof(ChaFileFace), baseProperties, prefix);

            generatedProperties.Add(
                new CategoryProperty(CategoryNo.st_eye, "Eye1", prefix),
                new StructValue<int>(
                    (obj, value) => { ((ChaFileFace)obj).pupil[0].pupilId = value; },
                    (obj) => ((ChaFileFace)obj).pupil[0].pupilId));

            generatedProperties.Add(
                new CategoryProperty(CategoryNo.st_eye, "Eye2", prefix),
                new StructValue<int>(
                    (obj, value) => { ((ChaFileFace)obj).pupil[1].pupilId = value; },
                    (obj) => ((ChaFileFace)obj).pupil[1].pupilId));

            generatedProperties.Add(
                new CategoryProperty(CategoryNo.st_eyeblack, "EyeBlack1", prefix),
                new StructValue<int>(
                    (obj, value) => { ((ChaFileFace)obj).pupil[0].blackId = value; },
                    (obj) => ((ChaFileFace)obj).pupil[0].blackId));

            generatedProperties.Add(
                new CategoryProperty(CategoryNo.st_eyeblack, "EyeBlack2", prefix),
                new StructValue<int>(
                    (obj, value) => { ((ChaFileFace)obj).pupil[1].blackId = value; },
                    (obj) => ((ChaFileFace)obj).pupil[1].blackId));

            return generatedProperties;
        }

        public static Dictionary<CategoryProperty, StructValue<int>> ChaFileFaceProperties { get; } = _chaFileFaceGenerator();
        #endregion

        #region ChaFileBody
        private static Dictionary<CategoryProperty, StructValue<int>> _chaFileBodyGenerator()
        {
            const string prefix = nameof(ChaFileBody);

            var baseProperties = new List<CategoryProperty>
            {
                new CategoryProperty(CategoryNo.ft_detail_f, "detailId"),
                new CategoryProperty(CategoryNo.st_nip, "nipId"),
                new CategoryProperty(CategoryNo.ft_skin_b, "skinId"),
                new CategoryProperty(CategoryNo.st_underhair, "underhairId"),
            };

            var generatedProperties = GeneratePropertyInfoDictionary(typeof(ChaFileBody), baseProperties, prefix);

            generatedProperties.Add(
                new CategoryProperty(CategoryNo.st_paint, "PaintID1", prefix),
                new StructValue<int>(
                    (obj, value) => { ((ChaFileBody)obj).paintInfo[0].id = value; },
                    (obj) => ((ChaFileBody)obj).paintInfo[0].id));

            generatedProperties.Add(
                new CategoryProperty(CategoryNo.st_paint, "PaintID2", prefix),
                new StructValue<int>(
                    (obj, value) => { ((ChaFileBody)obj).paintInfo[1].id = value; },
                    (obj) => ((ChaFileBody)obj).paintInfo[1].id));

            generatedProperties.Add(
                new CategoryProperty(CategoryNo.bodypaint_layout, "PaintLayoutID1", prefix),
                new StructValue<int>(
                    (obj, value) => { ((ChaFileBody)obj).paintInfo[0].layoutId = value; },
                    (obj) => ((ChaFileBody)obj).paintInfo[0].layoutId));

            generatedProperties.Add(
                new CategoryProperty(CategoryNo.bodypaint_layout, "PaintLayoutID2", prefix),
                new StructValue<int>(
                    (obj, value) => { ((ChaFileBody)obj).paintInfo[1].layoutId = value; },
                    (obj) => ((ChaFileBody)obj).paintInfo[1].layoutId));

            return generatedProperties;
        }

        public static Dictionary<CategoryProperty, StructValue<int>> ChaFileBodyProperties { get; } = _chaFileBodyGenerator();

        #endregion

        #region ChaFileHair
        private static Dictionary<CategoryProperty, StructValue<int>> _chaFileHairGenerator()
        {
            const string prefix = nameof(ChaFileHair);

            Dictionary<CategoryProperty, StructValue<int>> generatedProperties = new Dictionary<CategoryProperty, StructValue<int>>();

            generatedProperties.Add(
                new CategoryProperty(CategoryNo.so_hair_b, "HairBack", prefix),
                new StructValue<int>(
                    (obj, value) => { ((ChaFileHair)obj).parts[(int)HairKind.back].id = value; },
                    (obj) => ((ChaFileHair)obj).parts[(int)HairKind.back].id));

            generatedProperties.Add(
                new CategoryProperty(CategoryNo.so_hair_f, "HairFront", prefix),
                new StructValue<int>(
                    (obj, value) => { ((ChaFileHair)obj).parts[(int)HairKind.front].id = value; },
                    (obj) => ((ChaFileHair)obj).parts[(int)ChaFileDefine.HairKind.front].id));

            generatedProperties.Add(
                new CategoryProperty(CategoryNo.so_hair_s, "HairSide", prefix),
                new StructValue<int>(
                    (obj, value) => { ((ChaFileHair)obj).parts[(int)HairKind.side].id = value; },
                    (obj) => ((ChaFileHair)obj).parts[(int)HairKind.side].id));

            generatedProperties.Add(
                new CategoryProperty(CategoryNo.so_hair_o, "HairOption", prefix),
                new StructValue<int>(
                    (obj, value) => { ((ChaFileHair)obj).parts[(int)HairKind.option].id = value; },
                    (obj) => ((ChaFileHair)obj).parts[(int)HairKind.option].id));

            return generatedProperties;
        }

        public static Dictionary<CategoryProperty, StructValue<int>> ChaFileHairProperties { get; } = _chaFileHairGenerator();
        #endregion

        #region ChaFileMakeup
        private static Dictionary<CategoryProperty, StructValue<int>> _chaFileMakeupGenerator()
        {
            const string prefix = nameof(ChaFileFace.MakeupInfo);

            var baseProperties = new List<CategoryProperty>
            {
                new CategoryProperty(CategoryNo.st_cheek, "cheekId"),
                new CategoryProperty(CategoryNo.st_eyeshadow, "eyeshadowId"),
                new CategoryProperty(CategoryNo.st_lip, "lipId"),
            };

            var generatedProperties = GeneratePropertyInfoDictionary(typeof(ChaFileFace.MakeupInfo), baseProperties, prefix);

            generatedProperties.Add(
                new CategoryProperty(CategoryNo.st_paint, "PaintID1", prefix),
                new StructValue<int>(
                    (obj, value) => { ((ChaFileFace.MakeupInfo)obj).paintInfo[0].id = value; },
                    (obj) => ((ChaFileFace.MakeupInfo)obj).paintInfo[0].id));

            generatedProperties.Add(
                new CategoryProperty(CategoryNo.st_paint, "PaintID2", prefix),
                new StructValue<int>(
                    (obj, value) => { ((ChaFileFace.MakeupInfo)obj).paintInfo[1].id = value; },
                    (obj) => ((ChaFileFace.MakeupInfo)obj).paintInfo[1].id));

            return generatedProperties;
        }

        public static Dictionary<CategoryProperty, StructValue<int>> ChaFileMakeupProperties { get; } = _chaFileMakeupGenerator();
        #endregion

        #region ChaFileClothes
        private static Dictionary<CategoryProperty, StructValue<int>> _chaFileClothesGenerator()
        {
            const string prefix = nameof(ChaFileClothes);

            var generatedProperties = new Dictionary<CategoryProperty, StructValue<int>>();

            //main parts
            generatedProperties.Add(
                new CategoryProperty(CategoryNo.fo_top, "ClothesTop", prefix),
                new StructValue<int>(
                    (obj, value) => { ((ChaFileClothes)obj).parts[(int)ClothesKind.top].id = value; },
                    (obj) => ((ChaFileClothes)obj).parts[(int)ClothesKind.top].id));

            generatedProperties.Add(
                new CategoryProperty(CategoryNo.fo_bot, "ClothesBot", prefix),
                new StructValue<int>(
                    (obj, value) => { ((ChaFileClothes)obj).parts[(int)ClothesKind.bot].id = value; },
                    (obj) => ((ChaFileClothes)obj).parts[(int)ClothesKind.bot].id));

            generatedProperties.Add(
                new CategoryProperty(CategoryNo.fo_inner_t, "ClothesBra", prefix),
                new StructValue<int>(
                    (obj, value) => { ((ChaFileClothes)obj).parts[(int)ClothesKind.inner_t].id = value; },
                    (obj) => ((ChaFileClothes)obj).parts[(int)ClothesKind.inner_t].id));

            generatedProperties.Add(
                new CategoryProperty(CategoryNo.fo_inner_b, "ClothesShorts", prefix),
                new StructValue<int>(
                    (obj, value) => { ((ChaFileClothes)obj).parts[(int)ClothesKind.inner_b].id = value; },
                    (obj) => ((ChaFileClothes)obj).parts[(int)ClothesKind.inner_b].id));

            generatedProperties.Add(
                new CategoryProperty(CategoryNo.fo_gloves, "ClothesGloves", prefix),
                new StructValue<int>(
                    (obj, value) => { ((ChaFileClothes)obj).parts[(int)ClothesKind.gloves].id = value; },
                    (obj) => ((ChaFileClothes)obj).parts[(int)ClothesKind.gloves].id));

            generatedProperties.Add(
                new CategoryProperty(CategoryNo.fo_panst, "ClothesPantyhose", prefix),
                new StructValue<int>(
                    (obj, value) => { ((ChaFileClothes)obj).parts[(int)ClothesKind.panst].id = value; },
                    (obj) => ((ChaFileClothes)obj).parts[(int)ClothesKind.panst].id));

            generatedProperties.Add(
                new CategoryProperty(CategoryNo.fo_socks, "ClothesSocks", prefix),
                new StructValue<int>(
                    (obj, value) => { ((ChaFileClothes)obj).parts[(int)ClothesKind.socks].id = value; },
                    (obj) => ((ChaFileClothes)obj).parts[(int)ClothesKind.socks].id));

            generatedProperties.Add(
                new CategoryProperty(CategoryNo.fo_shoes, "ClothesShoes", prefix),
                new StructValue<int>(
                    (obj, value) => { ((ChaFileClothes)obj).parts[(int)ClothesKind.shoes].id = value; },
                    (obj) => ((ChaFileClothes)obj).parts[(int)ClothesKind.shoes].id));

            //Patterns
            for (int i = 0; i < 3; i++)
            {
                //we declare a separate value here instead of reusing `i` since closures and for variables don't mix very well
                //see this link for more info (it's specifically about foreach, but it applies here too)
                //https://stackoverflow.com/questions/12112881/has-foreachs-use-of-variables-been-changed-in-c-sharp-5
                int index = i;

                //top
                generatedProperties.Add(
                new CategoryProperty(CategoryNo.st_pattern, $"ClothesTopPattern{index}", prefix),
                new StructValue<int>(
                    (obj, value) => { ((ChaFileClothes)obj).parts[(int)ClothesKind.top].colorInfo[index].pattern = value; },
                    (obj) => ((ChaFileClothes)obj).parts[(int)ClothesKind.top].colorInfo[index].pattern));

                //bot
                generatedProperties.Add(
                    new CategoryProperty(CategoryNo.st_pattern, $"ClothesBotPattern{index}", prefix),
                    new StructValue<int>(
                        (obj, value) => { ((ChaFileClothes)obj).parts[(int)ClothesKind.bot].colorInfo[index].pattern = value; },
                        (obj) => ((ChaFileClothes)obj).parts[(int)ClothesKind.bot].colorInfo[index].pattern));

                //bra
                generatedProperties.Add(
                    new CategoryProperty(CategoryNo.st_pattern, $"ClothesBraPattern{index}", prefix),
                    new StructValue<int>(
                        (obj, value) => { ((ChaFileClothes)obj).parts[(int)ClothesKind.inner_t].colorInfo[index].pattern = value; },
                        (obj) => ((ChaFileClothes)obj).parts[(int)ClothesKind.inner_t].colorInfo[index].pattern));

                //shorts
                generatedProperties.Add(
                    new CategoryProperty(CategoryNo.st_pattern, $"ClothesShortsPattern{index}", prefix),
                    new StructValue<int>(
                        (obj, value) => { ((ChaFileClothes)obj).parts[(int)ClothesKind.inner_b].colorInfo[index].pattern = value; },
                        (obj) => ((ChaFileClothes)obj).parts[(int)ClothesKind.inner_b].colorInfo[index].pattern));

                //gloves
                generatedProperties.Add(
                    new CategoryProperty(CategoryNo.st_pattern, $"ClothesGlovesPattern{index}", prefix),
                    new StructValue<int>(
                        (obj, value) => { ((ChaFileClothes)obj).parts[(int)ClothesKind.gloves].colorInfo[index].pattern = value; },
                        (obj) => ((ChaFileClothes)obj).parts[(int)ClothesKind.gloves].colorInfo[index].pattern));

                //pants
                generatedProperties.Add(
                    new CategoryProperty(CategoryNo.st_pattern, $"ClothesPantyhosePattern{index}", prefix),
                    new StructValue<int>(
                        (obj, value) => { ((ChaFileClothes)obj).parts[(int)ClothesKind.panst].colorInfo[index].pattern = value; },
                        (obj) => ((ChaFileClothes)obj).parts[(int)ClothesKind.panst].colorInfo[index].pattern));

                //socks
                generatedProperties.Add(
                    new CategoryProperty(CategoryNo.st_pattern, $"ClothesSocksPattern{index}", prefix),
                    new StructValue<int>(
                        (obj, value) => { ((ChaFileClothes)obj).parts[(int)ClothesKind.socks].colorInfo[index].pattern = value; },
                        (obj) => ((ChaFileClothes)obj).parts[(int)ClothesKind.socks].colorInfo[index].pattern));


                //shoes
                generatedProperties.Add(
                    new CategoryProperty(CategoryNo.st_pattern, $"ClothesShoesPattern{index}", prefix),
                    new StructValue<int>(
                        (obj, value) => { ((ChaFileClothes)obj).parts[(int)ClothesKind.shoes].colorInfo[index].pattern = value; },
                        (obj) => ((ChaFileClothes)obj).parts[(int)ClothesKind.shoes].colorInfo[index].pattern));
            }

            return generatedProperties;
        }

        public static Dictionary<CategoryProperty, StructValue<int>> ChaFileClothesProperties { get; } = _chaFileClothesGenerator();
        #endregion

        #region ChaFileAccessory.PartsInfo
        private static Dictionary<CategoryProperty, StructValue<int>> _chaFileAccessoryPartsInfoGenerator()
        {
            string prefix = $"{nameof(ChaFileAccessory)}.{nameof(ChaFileAccessory.PartsInfo)}";

            var baseProperties = new List<CategoryProperty>
            {
                new CategoryProperty(CategoryNo.ao_none , "id", prefix),
                new CategoryProperty(CategoryNo.ao_head, "id", prefix),
                new CategoryProperty(CategoryNo.ao_ear, "id", prefix),
                new CategoryProperty(CategoryNo.ao_glasses, "id", prefix),
                new CategoryProperty(CategoryNo.ao_face, "id", prefix),
                new CategoryProperty(CategoryNo.ao_neck, "id", prefix),
                new CategoryProperty(CategoryNo.ao_shoulder, "id", prefix),
                new CategoryProperty(CategoryNo.ao_waist, "id", prefix),
                new CategoryProperty(CategoryNo.ao_back, "id", prefix),
                new CategoryProperty(CategoryNo.ao_arm, "id", prefix),
                new CategoryProperty(CategoryNo.ao_hand, "id", prefix),
                new CategoryProperty(CategoryNo.ao_leg, "id", prefix),
                new CategoryProperty(CategoryNo.ao_kokan, "id", prefix)
            };

            var generatedProperties = GeneratePropertyInfoDictionary(typeof(ChaFileAccessory.PartsInfo), baseProperties, prefix);

            return generatedProperties;
        }

        public static Dictionary<CategoryProperty, StructValue<int>> ChaFileAccessoryPartsInfoProperties { get; } = _chaFileAccessoryPartsInfoGenerator();
        #endregion

        #region Collated
        private static Dictionary<CategoryProperty, StructValue<int>> _collatedGenerator()
        {
            var collated = new Dictionary<CategoryProperty, StructValue<int>>();

            foreach (var kv in ChaFileFaceProperties) collated.Add(kv.Key, kv.Value);
            foreach (var kv in ChaFileBodyProperties) collated.Add(kv.Key, kv.Value);
            foreach (var kv in ChaFileHairProperties) collated.Add(kv.Key, kv.Value);

            foreach (var kv in ChaFileClothesProperties) collated.Add(kv.Key, kv.Value);
            foreach (var kv in ChaFileMakeupProperties) collated.Add(kv.Key, kv.Value);
            foreach (var kv in ChaFileAccessoryPartsInfoProperties) collated.Add(kv.Key, kv.Value);

            return collated;
        }

        public static Dictionary<CategoryProperty, StructValue<int>> CollatedStructValues { get; } = _collatedGenerator();
        #endregion
    }
}