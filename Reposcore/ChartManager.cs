using System;
using System.Collections.Generic;
using ScottPlot;
using ScottPlot.Drawing; // ScottPlot의 Color 타입과 Colors 팔레트 사용

namespace Reposcore
{
    public enum ChartTheme
    {
        Default,
        Dark,
        Colorful,
        Random
    }

    /// <summary>
    /// 차트의 색상 및 테마 커스터마이징을 담당합니다.
    /// </summary>
    public static class ChartManager
    {
        // 사용자별 고정 색상 매핑 (대소문자 구분 없이)
        private static readonly Dictionary<string, Color> UserColors =
            new(StringComparer.OrdinalIgnoreCase);

        // 기본 팔레트
        private static readonly Color[] DefaultPalette = {
            Colors.SteelBlue,
            Colors.MediumSeaGreen,
            Colors.Coral,
            Colors.Goldenrod
        };

        // Dark 테마용 팔레트
        private static readonly Color[] DarkPalette = {
            Colors.LightSkyBlue,
            Colors.LightGreen,
            Colors.LightCoral,
            Colors.Khaki
        };

        private static readonly Random Rng = new();

        /// <summary>
        /// 지정된 테마를 Plot 객체에 적용합니다.
        /// </summary>
        public static void ApplyTheme(Plot plt, ChartTheme theme)
        {
            if (theme == ChartTheme.Dark)
            {
                plt.Style(
                    figureBackground: Colors.Black,
                    dataBackground:   Colors.DimGray,
                    tickColor:        Colors.White,
                    frameColor:       Colors.White
                );
                plt.TitleColor(Colors.White);
                plt.XLabelColor(Colors.White);
                plt.YLabelColor(Colors.White);
            }
            // Default, Colorful, Random 테마는 필요 시 구현하세요.
        }

        /// <summary>
        /// 사용자별, 테마별 바 색상을 반환합니다.
        /// </summary>
        public static Color GetBarColor(string userKey, ChartTheme theme = ChartTheme.Default)
        {
            // 이미 매핑된 색상이 있으면 그대로 반환
            if (UserColors.TryGetValue(userKey, out var c))
                return c;

            // 테마에 따른 팔레트 선택
            Color[] palette = theme switch
            {
                ChartTheme.Dark     => DarkPalette,
                ChartTheme.Colorful => DefaultPalette,
                ChartTheme.Random   => DefaultPalette,
                _                   => DefaultPalette
            };

            // 랜덤 테마면 무작위 색 선택, 아니면 순환 할당
            if (theme == ChartTheme.Random)
                c = palette[Rng.Next(palette.Length)];
            else
                c = palette[UserColors.Count % palette.Length];

            UserColors[userKey] = c;
            return c;
        }
    }
}
