/*
 *      Copyright (C) 2020 Dieter (coder2000) Lunn
 *
 *      This program is free software: you can redistribute it and/or modify
 *      it under the terms of the GNU General Public License as published by
 *      the Free Software Foundation, either version 3 of the License, or
 *      (at your option) any later version.
 *
 *      This program is distributed in the hope that it will be useful,
 *      but WITHOUT ANY WARRANTY; without even the implied warranty of
 *      MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *      GNU General Public License for more details.
 *
 *      You should have received a copy of the GNU General Public License
 *      along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;

using Ubiety.Logging.Core;

namespace Ubiety.Dns.Core
{
    /// <summary>
    ///     Builds a resolver instance.
    /// </summary>
    public class ResolverBuilder
    {
        private readonly List<IPEndPoint> _dnsServers;
        private IUbietyLogManager _logManager;
        private int _timeout;
        private bool _enableCache;
        private int _retries;
        private bool _useRecursion;

        private ResolverBuilder()
        {
            _dnsServers = new List<IPEndPoint>();
        }

        /// <summary>
        ///     Begin building a DNS resolver.
        /// </summary>
        /// <returns>A <see cref="ResolverBuilder"/> instance.</returns>
        public static ResolverBuilder Begin()
        {
            return new ResolverBuilder();
        }

        /// <summary>
        ///     Enable logging for the resolver.
        /// </summary>
        /// <param name="logManager"><see cref="IUbietyLogManager"/> instance to use for logging.</param>
        /// <returns>The current <see cref="ResolverBuilder"/> instance.</returns>
        public ResolverBuilder EnableLogging(IUbietyLogManager logManager)
        {
            _logManager = logManager;
            return this;
        }

        /// <summary>
        ///     Add a DNS server to the resolver.
        /// </summary>
        /// <param name="server"><see cref="IPEndPoint" /> of the server.</param>
        /// <returns>The current <see cref="ResolverBuilder" /> instance.</returns>
        public ResolverBuilder AddDnsServer(IPEndPoint server)
        {
            _dnsServers.Add(server);

            return this;
        }

        /// <summary>
        ///     Add a DNS server to the resolver.
        /// </summary>
        /// <param name="serverAddress"><see cref="IPAddress" /> of the server.</param>
        /// <param name="port">Port of the server, defaults to 53.</param>
        /// <returns>The current <see cref="ResolverBuilder" /> instance.</returns>
        public ResolverBuilder AddDnsServer(IPAddress serverAddress, int port = 53)
        {
            _dnsServers.Add(new IPEndPoint(serverAddress, port));

            return this;
        }

        /// <summary>
        ///     Add a DNS server to the resolver.
        /// </summary>
        /// <param name="serverAddress">String representing the ip address of the server.</param>
        /// <param name="port">Port of the server, defaults to 53.</param>
        /// <returns>The current <see cref="ResolverBuilder" /> instance.</returns>
        public ResolverBuilder AddDnsServer(string serverAddress, int port = 53)
        {
            if (IPAddress.TryParse(serverAddress, out var serverIp))
            {
                _dnsServers.Add(new IPEndPoint(serverIp, port));
            }

            return this;
        }

        /// <summary>
        ///     Add multiple DNS servers to the resolver.
        /// </summary>
        /// <param name="dnsServers"><see cref="IEnumerable" /> with the endpoints.</param>
        /// <returns>The current <see cref="ResolverBuilder" /> instance.</returns>
        public ResolverBuilder AddDnsServers(IEnumerable<IPEndPoint> dnsServers)
        {
            _dnsServers.AddRange(dnsServers);

            return this;
        }

        /// <summary>
        ///     Set a timeout in milliseconds for TCP requests.
        /// </summary>
        /// <param name="timeout">Time in milliseconds to wait for a response.</param>
        /// <returns>The current <see cref="ResolverBuilder" /> instance.</returns>
        public ResolverBuilder SetTimeout(int timeout)
        {
            _timeout = timeout;

            return this;
        }

        /// <summary>
        ///     Enable caching of DNS responses.
        /// </summary>
        /// <returns>The current <see cref="ResolverBuilder" /> instance.</returns>
        public ResolverBuilder EnableCache()
        {
            _enableCache = true;

            return this;
        }

        /// <summary>
        ///     Set the number of times a request should be tried before failure.
        /// </summary>
        /// <param name="retries">Tries to use.</param>
        /// <returns>The current <see cref="ResolverBuilder" /> instance.</returns>
        public ResolverBuilder SetRetries(int retries)
        {
            _retries = retries;

            return this;
        }

        /// <summary>
        ///     Use recursion when resolving queries.
        /// </summary>
        /// <returns>The current <see cref="ResolverBuilder" /> instance.</returns>
        public ResolverBuilder UseRecursion()
        {
            _useRecursion = true;

            return this;
        }

        /// <summary>
        ///     Build the resolver instance with options provided.
        /// </summary>
        /// <returns>A <see cref="Resolver"/> instance.</returns>
        public Resolver Build()
        {
            if (_logManager != null)
            {
                UbietyLogger.Initialize(_logManager);
            }

            if (_dnsServers.Count < 1)
            {
                AddSystemServers();
            }

            if (_timeout == 0)
            {
                _timeout = 1000;
            }

            if (_retries == 0)
            {
                _retries = 1;
            }

            return new Resolver(_dnsServers) { Timeout = _timeout, UseCache = _enableCache, Retries = _retries, Recursion = _useRecursion };
        }

        private void AddSystemServers()
        {
            var interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var adapter in interfaces)
            {
                if (adapter.OperationalStatus != OperationalStatus.Up)
                {
                    continue;
                }

                var interfaceProperties = adapter.GetIPProperties();

                // thanks to Jon Webster on May 20, 2008
                foreach (var address in interfaceProperties.DnsAddresses)
                {
                    var entry = new IPEndPoint(address, 53);
                    if (!_dnsServers.Contains(entry))
                    {
                        _dnsServers.Add(entry);
                    }
                }
            }
        }
    }
}
