﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tao.Core;
using BinaryReader = Tao.Core.BinaryReader;

namespace Tao.UnitTests
{
    [TestFixture]
    public class ModuleTableReadTests : BaseHeaderReadTest
    {
        [Test]
        public void ShouldReadModuleName()
        {
            var stream = OpenSampleAssembly();

            var heap = new StringHeap();
            var reader = new BinaryReader(stream);
            var table = new ModuleTable(new MetadataStream(), heap);
            heap.ReadFrom(reader);
            reader.Seek(0, SeekOrigin.Begin);
            table.ReadFrom(reader);

            var row = table.Rows[0];
            Assert.AreEqual(row.ModuleName, "skeleton.exe");
            stream.Dispose();
            stream.Close();
        }

        [Test]
        public void ShouldReadGeneration()
        {
            AssertEquals<ModuleTable, ushort?>(0, t => t.Rows[0].Generation);
        }

        [Test]
        public void ShouldReadNameIndex()
        {
            AssertEquals<ModuleTable, uint?>(0xA, t => t.Rows[0].NameIndex.Value);
        }

        [Test]
        public void ShouldReadMvid()
        {
            AssertEquals<ModuleTable, uint?>(1, t => t.Rows[0].Mvid.Value);
        }

        [Test]
        public void ShouldReadEncId()
        {
            AssertEquals<ModuleTable, uint?>(0, t => t.Rows[0].EncId.Value);
        }

        [Test]
        public void ShouldReadEncBaseId()
        {
            AssertEquals<ModuleTable, uint?>(0, t => t.Rows[0].EncBaseId.Value);
        }

        protected override void SetStreamPosition(Stream stream)
        {
            stream.Seek(0x2f4, SeekOrigin.Begin);
        }
    }
}