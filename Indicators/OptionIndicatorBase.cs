/*
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
*/

using System;
using QuantConnect.Data;

namespace QuantConnect.Indicators
{
    /// <summary>
    /// To provide a base class for option indicator
    /// </summary>
    public abstract class OptionIndicatorBase : IndicatorBase<IndicatorDataPoint>, IIndicatorWarmUpPeriodProvider
    {
        /// <summary>
        /// Option's symbol object
        /// </summary>
        protected readonly Symbol _optionSymbol;

        /// <summary>
        /// Underlying security's symbol object
        /// </summary>
        protected Symbol _underlyingSymbol => _optionSymbol.Underlying;

        /// <summary>
        /// Option pricing model used to calculate indicator
        /// </summary>
        protected OptionPricingModelType _optionModel;

        /// <summary>
        /// Risk-free rate model
        /// </summary>
        protected readonly IRiskFreeInterestRateModel _riskFreeInterestRateModel;

        /// <summary>
        /// Gets the expiration time of the option
        /// </summary>
        public DateTime Expiry => _optionSymbol.ID.Date;

        /// <summary>
        /// Gets the option right (call/put) of the option
        /// </summary>
        public OptionRight Right => _optionSymbol.ID.OptionRight;

        /// <summary>
        /// Gets the strike price of the option
        /// </summary>
        public decimal Strike => _optionSymbol.ID.StrikePrice;

        /// <summary>
        /// Gets the option style (European/American) of the option
        /// </summary>
        public OptionStyle Style => _optionSymbol.ID.OptionStyle;

        /// <summary>
        /// Risk Free Rate
        /// </summary>
        public Identity RiskFreeRate { get; set; }

        /// <summary>
        /// Gets the option price level
        /// </summary>
        public IndicatorBase<IndicatorDataPoint> Price { get; }

        /// <summary>
        /// Gets the underlying's price level
        /// </summary>
        public IndicatorBase<IndicatorDataPoint> UnderlyingPrice { get; }

        /// <summary>
        /// Initializes a new instance of the OptionIndicatorBase class
        /// </summary>
        /// <param name="name">The name of this indicator</param>
        /// <param name="option">The option to be tracked</param>
        /// <param name="riskFreeRateModel">Risk-free rate model</param>
        /// <param name="period">The lookback period of volatility</param>
        /// <param name="optionModel">The option pricing model used to estimate the Greek/IV</param>
        protected OptionIndicatorBase(string name, Symbol option, IRiskFreeInterestRateModel riskFreeRateModel, int period = 2,
            OptionPricingModelType optionModel = OptionPricingModelType.BlackScholes)
            : base(name)
        {
            var sid = option.ID;
            if (!sid.SecurityType.IsOption())
            {
                throw new ArgumentException("OptionIndicatorBase only support SecurityType.Option.");
            }

            _optionSymbol = option;
            _riskFreeInterestRateModel = riskFreeRateModel;
            _optionModel = optionModel;

            RiskFreeRate = new Identity(name + "_RiskFreeRate");
            Price = new Identity(name + "_Close");
            UnderlyingPrice = new Identity(name + "_UnderlyingClose");

            WarmUpPeriod = period;
        }

        /// <summary>
        /// Required period, in data points, for the indicator to be ready and fully initialized.
        /// </summary>
        public int WarmUpPeriod { get; set; }

        /// <summary>
        /// Resets this indicator and all sub-indicators
        /// </summary>
        public override void Reset()
        {
            RiskFreeRate.Reset();
            Price.Reset();
            UnderlyingPrice.Reset();
            base.Reset();
        }
    }
}
