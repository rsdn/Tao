﻿using System.IO;
using Tao.Interfaces;

namespace Tao.Seekers
{
    /// <summary>
    /// Represents a <see cref="IFunction{TInput}"/> that can find the coff header position.
    /// </summary>
    public class OptionalHeaderSeeker : IFunction<Stream>
    {
        /// <summary>
        /// Seeks the coff header position within a given stream.
        /// </summary>
        /// <param name="stream">The target stream.</param>
        public void Execute(Stream stream)
        {
            stream.Seek(0x98, SeekOrigin.Begin);
        }
    }
}