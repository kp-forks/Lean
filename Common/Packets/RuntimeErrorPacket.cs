﻿/*
 * QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals.
 * Lean Algorithmic Trading Engine v2.0. Copyright 2014 QuantConnect Corporation.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); 
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
*/

using Newtonsoft.Json;

namespace QuantConnect.Packets
{
    /******************************************************** 
    * QUANTCONNECT WORKER QUEUE BASE CLASS:
    *********************************************************/
    /// <summary>
    /// Algorithm runtime error packet from the lean engine. 
    /// This is a managed error which stops the algorithm execution.
    /// </summary>
    public class RuntimeErrorPacket : Packet
    {
        /******************************************************** 
        * CLASS VARIABLES
        *********************************************************/
        /// <summary>
        /// Runtime error message from the exception
        /// </summary>
        [JsonProperty(PropertyName = "sMessage")]
        public string Message;

        /// <summary>
        /// Algorithm id which generated this runtime error
        /// </summary>
        [JsonProperty(PropertyName = "sAlgorithmID")]
        public string AlgorithmId;

        /// <summary>
        /// Error stack trace information string passed through from the Lean exception
        /// </summary>
        [JsonProperty(PropertyName = "sStackTrace")]
        public string StackTrace;

        /******************************************************** 
        * CLASS CONSTRUCTOR
        *********************************************************/
        /// <summary>
        /// Default constructor for JSON
        /// </summary>
        public RuntimeErrorPacket()
            : base (PacketType.RuntimeError)
        { }

        /// <summary>
        /// Create a new runtime error packet
        /// </summary>
        public RuntimeErrorPacket(string algorithmId, string message, string stacktrace = "")
            : base(PacketType.RuntimeError)
        {
            Message = message;
            AlgorithmId = algorithmId;
            StackTrace = stacktrace;
        }
    
    } // End Work Packet:

} // End of Namespace:
