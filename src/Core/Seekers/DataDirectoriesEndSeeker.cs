﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Core.Seekers
{
    /// <summary>
    /// Represents a stream seeker class that sets the stream position to point to the end of the list of data directory headers in a portable executable file.
    /// </summary>
    public class DataDirectoriesEndSeeker : IStreamSeeker
    {
        private readonly IFunction<Stream, int> _dataDirectoryCounter;
        private readonly IStreamSeeker _dataDirectoriesSeeker;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        /// <param name="dataDirectoryCounter">The object that will be responsible for determining the available number of data directories from a given portable executable stream.</param>
        /// <param name="dataDirectoriesSeeker">The <see cref="IStreamSeeker"/> that will set the stream position to point to the first byte in the data directory headers.</param>
        public DataDirectoriesEndSeeker(IFunction<Stream, int> dataDirectoryCounter, IStreamSeeker dataDirectoriesSeeker)
        {
            _dataDirectoryCounter = dataDirectoryCounter;
            _dataDirectoriesSeeker = dataDirectoriesSeeker;
        }

        /// <summary>
        /// Sets the stream position to point to the end of the list of data directory headers in a portable executable file.
        /// </summary>
        /// <param name="stream">The target stream.</param>
        public void Seek(Stream stream)
        {
            var numberOfDirectories = _dataDirectoryCounter.Execute(stream);
            const int dataDirectorySize = 8;

            var targetPosition = numberOfDirectories * dataDirectorySize;

            _dataDirectoriesSeeker.Seek(stream);
            stream.Seek(targetPosition, SeekOrigin.Current);
        }
    }
}
