﻿using FourierCircles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Xunit;

namespace FourierCirclesTests
{
    public class SeriesTests
    {
        [Fact]
        public void AddHarmonics()
        {
            var roi = new Series();
            var firstHarmonic = roi.AddHarmonic(1, 0, 1);
            var secondHarmonic = roi.AddHarmonic(1, 0, 2);
            Assert.Contains(firstHarmonic,roi.Harmonics);
            Assert.Contains(secondHarmonic, roi.Harmonics);
        }
        
        [Fact]
        public void ChainHarmonics()
        {
            var roi = new Series();
            var firstHarmonic = roi.AddHarmonic(1, 0, 1);
            roi.AddHarmonic(1, 0, 1);
            roi.AddHarmonic(1, 0, 1);
            Assert.NotSame(firstHarmonic.SubHarmonic.ParentHarmonic, firstHarmonic.SubHarmonic.SubHarmonic);
            Assert.Null(firstHarmonic.ParentHarmonic);
            Assert.Equal(firstHarmonic.SubHarmonic.Origin, firstHarmonic.End);
            Assert.Equal(firstHarmonic.SubHarmonic.SubHarmonic.Origin, firstHarmonic.SubHarmonic.End);
            Assert.Equal(new Point(0, 3), firstHarmonic.SubHarmonic.SubHarmonic.End);
        }

        [Fact]
        public void TickTock()
        {
            var roi = new Series();
            var last = roi.AddHarmonic(1, 0, 90);
            roi.Tick();
            Assert.Equal(1, last.End.X, 1);
            Assert.Equal(0, last.End.Y, 1);
        }

        [Fact]
        public void Formula()
        {
            var roi = new Series();
            var offset = 0;
            var amplify = 11;
            var phase = 90;
            var freq = 1;
            roi.Initiate(offset,amplify,phase,freq);
            //roi.Expression((a, n) => a / (2 * n - 1) / Math.PI, (t, n) => 2 * n - 1);
            roi.Expression((a, n) => a - n, (t, n) => t + n);//.Times(10);
            roi.Times(10);
            Assert.Equal(10, roi.Harmonics.Count());
            Assert.Equal(1, roi.Harmonics.Last().Length);
            Assert.Equal(11, roi.Harmonics.Last().Freq);
        }
    }
}
