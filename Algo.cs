// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
Main(args);


partial class Program
{
    static void Main(string[] args)
    {
        // Set up historical price data
        decimal[] prices = { 100.0m, 110.0m, 120.0m, 105.0m, 120.0m, 110.0m, 115.0m, 120.0m, 125.0m, 110.0m, 120.0m, 130.0m, 110.0m, 125.0m, 120.0m, 120.0m, 115.0m, 100.0m, 110.0m, 120.0m, 105.0m, 120.0m, 110.0m, 115.0m, 120.0m, 125.0m, 110.0m, 120.0m, 130.0m, 110.0m, 125.0m, 120.0m, 120.0m, 115.0m };

        // Set up RSI parameters
        int[] periods = { 14, 21, 28 };

        // Calculate RSI values
        decimal[][] rsis = CalculateRSIs(prices, periods);

        // Print RSI values
        for (int i = periods.Max() - 1; i < prices.Length; i++)
        {
            Console.WriteLine("RSI values for {0}:", prices[i]);

            for (int j = 0; j < periods.Length; j++)
            {
                Console.WriteLine("  {0}-period RSI: {1}", periods[j], rsis[j][i]);
            }

            Console.WriteLine();
        }


        Console.WriteLine("Second Method RSI");
        // Print RSI values
        for (int i = periods.Max() - 1; i < prices.Length; i++)
        {
            Console.WriteLine("RSI values for {0}:", prices[i]);

            for (int j = 0; j < periods.Length; j++)
            {
                var rsi = CalculateRSI(prices, periods[j]);
                Console.WriteLine("  {0}-period RSI: {1}", periods[j], rsi);
            }

            Console.WriteLine();
        }



        // Set up historical price data
        decimal[] pricesObv = { 100.0m, 105.0m, 110.0m, 115.0m, 120.0m, 125.0m, 130.0m, 125.0m, 120.0m, 115.0m, 100.0m, 105.0m, 110.0m, 115.0m, 120.0m, 125.0m, 130.0m, 125.0m, 120.0m, 115.0m, 100.0m, 105.0m, 110.0m, 115.0m, 120.0m, 125.0m, 130.0m, 125.0m, 120.0m, 115.0m };

        // Set up OBV parameters
        decimal[] volumesObv = { 1000.0m, 1500.0m, 2000.0m, 2500.0m, 3000.0m, 3500.0m, 4000.0m, 3500.0m, 3000.0m, 2500.0m };

        // Calculate OBV values
        decimal[] obvs = CalculateOBV(pricesObv, volumesObv);

        // Print OBV values
        for (int i = 0; i < prices.Length; i++)
        {
            Console.WriteLine("OBV for {0}: {1}", prices[i], obvs[i]);
        }


        Console.ReadLine();






        // Example input data
        decimal[] pricesSR = { 100.5m, 99.8m, 101.2m, 103.4m, 101.1m, 99.5m, 98.2m, 100.8m, 99.1m, 97.3m };
        int lookbackPeriod = 5;
        decimal supportStrengthThreshold = 0.5m;

        // Call IdentifySupportLevels function
        List<decimal> strongSupportLevels;
        List<decimal> weakSupportLevels;
        IdentifySupportLevels(prices, lookbackPeriod, supportStrengthThreshold, out strongSupportLevels, out weakSupportLevels);

        // Print results
        Console.WriteLine("Strong support levels:");
        foreach (decimal level in strongSupportLevels)
        {
            Console.WriteLine(level);
        }

        Console.WriteLine("Weak support levels:");
        foreach (decimal level in weakSupportLevels)
        {
            Console.WriteLine(level);
        }

        Console.ReadLine();
    }
    static void FindHighVolitileStocks()
    {

        Dictionary<string, decimal[]> priceHistories = new Dictionary<string, decimal[]>();
        priceHistories.Add("AAPL", new decimal[] { 120.5m, 122.3m, 118.9m, 121.4m, 119.8m });
        priceHistories.Add("GOOG", new decimal[] { 1401.2m, 1399.4m, 1412.6m, 1410.1m, 1398.5m });
        priceHistories.Add("AMZN", new decimal[] { 3100.3m, 3112.1m, 3089.9m, 3098.2m, 3115.8m });
        // ... add more stock price histories ...

        // Compute intraday price ranges for each stock
        Dictionary<string, decimal[]> priceRanges = new Dictionary<string, decimal[]>();
        foreach (string stockSymbol in priceHistories.Keys)
        {
            decimal[] prices = priceHistories[stockSymbol];
            decimal[] ranges = new decimal[prices.Length - 1];
            for (int i = 0; i < prices.Length - 1; i++)
            {
                ranges[i] = Math.Abs(prices[i + 1] - prices[i]);
            }
            priceRanges.Add(stockSymbol, ranges);
        }

        // Compute standard deviation of intraday price ranges
        double[] stdDevs = new double[priceHistories.Count];
        int index = 0;
        foreach (string stockSymbol in priceHistories.Keys)
        {
            stdDevs[index] = GetStdDev(priceRanges[stockSymbol]);
            index++;
        }

        // Find stock with highest intraday price variation
        string maxStdDevSymbol = "";
        double maxStdDev = double.MinValue;
        index = 0;
        foreach (string stockSymbol in priceHistories.Keys)
        {
            if (stdDevs[index] > maxStdDev)
            {
                maxStdDev = stdDevs[index];
                maxStdDevSymbol = stockSymbol;
            }
            index++;
        }

        // Print result
        Console.WriteLine("Stock with highest intraday price variation: " + maxStdDevSymbol);

        Console.ReadLine();
    }

    static void IdentifySupportLevels(decimal[] prices, int lookbackPeriod, int supportStrengthThreshold, out List<decimal> strongSupportLevels, out List<decimal> weakSupportLevels)
    {
       
    }

    static double GetStdDev(decimal[] values)
    {
        double mean = (double)values.Average();
        double sumOfSquaresOfDifferences = values.Select(val => (double)val).Select(val => (val - mean) * (val - mean)).Sum();
        return Math.Sqrt(sumOfSquaresOfDifferences / (values.Length - 1));
    }
    /// <summary>
    ///    // The function takes in an array of historical prices, a lookback period(the number of previous prices to consider), 
    //and a support strength threshold(the minimum distance between the support level and the prices to be considered 
    //a strong support level). It outputs two lists: one for strong support levels and one for weak support levels.
    //The function loops through each price in the input array and looks back at the previous lookbackPeriod prices to 
    //find the minimum price.It then checks if the current price and the previous prices are above the minimum price 
    //plus the supportStrengthThreshold.If they are, the function considers the minimum price to be a weak support level. 
    //If all the previous prices are above the minimum price plus supportStrengthThreshold, the function considers the 
    //minimum price to be a strong support level.
    //Note that this is just one way to identify support levels, and there are many other methods and 
    //variations that could be used. This function is provided as an example to get you started.
    /// </summary>
    /// <param name="prices"></param>
    /// <param name="lookbackPeriod"></param>
    /// <param name="supportStrengthThreshold"></param>
    /// <param name="strongSupportLevels"></param>
    /// <param name="weakSupportLevels"></param>
    static void IdentifySupportLevels(decimal[] prices, int lookbackPeriod, decimal supportStrengthThreshold, out List<decimal> strongSupportLevels, out List<decimal> weakSupportLevels)
    {
        strongSupportLevels = new List<decimal>();
        weakSupportLevels = new List<decimal>();

        for (int i = lookbackPeriod; i < prices.Length; i++)
        {
            decimal minPrice = prices[i - lookbackPeriod];
            for (int j = i - lookbackPeriod + 1; j <= i; j++)
            {
                if (prices[j] < minPrice)
                {
                    minPrice = prices[j];
                }
            }

            bool isStrongSupport = true;
            bool isWeakSupport = true;

            for (int j = i - lookbackPeriod; j < i; j++)
            {
                if (prices[j] < minPrice + supportStrengthThreshold)
                {
                    isStrongSupport = false;
                }

                if (prices[j] < minPrice + supportStrengthThreshold / 2)
                {
                    isWeakSupport = false;
                }
            }

            if (isStrongSupport)
            {
                strongSupportLevels.Add(minPrice);
            }
            else if (isWeakSupport)
            {
                weakSupportLevels.Add(minPrice);
            }
        }
    }
    static decimal[] CalculateOBV(decimal[] prices, decimal[] volumes)
    {
        decimal[] obvs = new decimal[prices.Length];

        obvs[0] = volumes[0];

        for (int i = 1; i < prices.Length; i++)
        {
            if (prices[i] > prices[i - 1])
            {
                obvs[i] = obvs[i - 1] + volumes[i];
            }
            else if (prices[i] < prices[i - 1])
            {
                obvs[i] = obvs[i - 1] - volumes[i];
            }
            else
            {
                obvs[i] = obvs[i - 1];
            }
        }

        return obvs;
    }
    static decimal[] CalculateRSI(decimal[] prices, int period)
    {
        decimal[] rsis = new decimal[prices.Length];

        decimal[] gains = new decimal[prices.Length];
        decimal[] losses = new decimal[prices.Length];

        for (int i = 1; i < prices.Length; i++)
        {
            decimal diff = prices[i] - prices[i - 1];

            if (diff > 0)
            {
                gains[i] = diff;
            }
            else
            {
                losses[i] = -diff;
            }
        }

        decimal sumGain = 0;
        decimal sumLoss = 0;

        for (int i = 1; i <= period; i++)
        {
            sumGain += gains[i];
            sumLoss += losses[i];
        }

        decimal avgGain = sumGain / period;
        decimal avgLoss = sumLoss / period;

        decimal rs = avgGain / avgLoss;
        rsis[period] = 100 - (100 / (1 + rs));

        for (int i = period + 1; i < prices.Length; i++)
        {
            avgGain = ((period - 1) * avgGain + gains[i]) / period;
            avgLoss = ((period - 1) * avgLoss + losses[i]) / period;

            rs = avgGain / avgLoss;
            rsis[i] = 100 - (100 / (1 + rs));
        }

        return rsis;
    }
    static decimal[][] CalculateRSIs(decimal[] prices, int[] periods)
    {
        decimal[][] rsis = new decimal[periods.Length][];

        for (int i = 0; i < periods.Length; i++)
        {
            rsis[i] = new decimal[prices.Length];

            decimal[] gains = new decimal[prices.Length];
            decimal[] losses = new decimal[prices.Length];

            for (int j = 1; j < prices.Length; j++)
            {
                decimal diff = prices[j] - prices[j - 1];

                if (diff >= 0)
                {
                    gains[j] = diff;
                    losses[j] = 0.0m;
                }
                else
                {
                    gains[j] = 0.0m;
                    losses[j] = -diff;
                }
            }

            decimal[] avgGains = CalculateEMA(gains, periods[i]);
            decimal[] avgLosses = CalculateEMA(losses, periods[i]);

            for (int j = periods[i] - 1; j < prices.Length; j++)
            {
                decimal rs = avgGains[j] / avgLosses[j];
                rsis[i][j] = 100.0m - (100.0m / (1.0m + rs));
            }
        }

        return rsis;
    }

    static decimal[] CalculateEMA(decimal[] values, int period)
    {
        decimal[] ema = new decimal[values.Length];

        decimal multiplier = 2.0m / (period + 1);

        ema[period - 1] = CalculateSMA(values, period);

        for (int i = period; i < values.Length; i++)
        {
            ema[i] = (values[i] - ema[i - 1]) * multiplier + ema[i - 1];
        }

        return ema;
    }

    static decimal CalculateSMA(decimal[] values, int period)
    {
        decimal sum = 0.0m;
        for (int i = values.Length - period; i < values.Length; i++)
        {
            sum += values[i];
        }

        return sum / period;
    }



}
