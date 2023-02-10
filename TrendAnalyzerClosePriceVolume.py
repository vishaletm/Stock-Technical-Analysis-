import pandas as pd

import numpy as np

import matplotlib.pyplot as plt

import talib

def trend_detector(close_prices, volumes):

    # Calculate the moving average convergence divergence (MACD)

    macd, signal, hist = talib.MACD(close_prices)

    

    # Calculate the relative strength index (RSI)

    rsi = talib.RSI(close_prices)

    

    # Calculate the moving average

    ma = talib.SMA(close_prices)

    

    # Combine the indicators to determine the trend

    trend = []

    for i in range(len(close_prices)):

        if (macd[i] > signal[i]) & (rsi[i] > 50) & (close_prices[i] > ma[i]) & (volumes[i] > np.mean(volumes)):

            trend.append("Uptrend")

        else:

            trend.append("Downtrend")

    return trend

def backtest(close_prices, volumes, trend):

    # Initialize a list to store the results of the backtest

    results = []

    

    # Loop through the close prices and volumes

    for i in range(1, len(close_prices)):

        if trend[i-1] == "Uptrend":

            if close_prices[i] > close_prices[i-1]:

                results.append(1)

            else:

                results.append(0)

        else:

            if close_prices[i] < close_prices[i-1]:

                results.append(1)

            else:

                results.append(0)

    

    # Calculate the success rate of the backtest

    success_rate = np.mean(results)

    return success_rate

def plot_results(close_prices, trend):

    # Create a dataframe from the close prices and trend

    df = pd.DataFrame({'Close Prices': close_prices, 'Trend': trend})

    

    # Plot the close prices and trend

    fig, ax1 = plt.subplots()

    color = 'tab:red'

    ax1.set_xlabel('Time')

    ax1.set_ylabel('Close Prices', color=color)

    ax1.plot(df['Close Prices'], color=color)

    ax1.tick_params(axis='y', labelcolor=color)

    

    ax2 = ax1.twinx()

    color = 'tab:blue'

    ax2.set_ylabel('Trend', color=color)

    ax2.plot(df['Trend'], color=color, marker='o', linestyle='None')

    ax2.tick_params(axis='y', labelcolor=color)

    

    fig.tight_layout()

    plt.show()

# Example usage


# Example usage
close_prices = [100, 105, 110, 100, 95, 105, 115, 120, 115, 110]
volumes = [1000, 2000, 1500, 1700, 1500, 1600, 1700, 1800, 1700, 1600]
trend = trend_detector(close_prices, volumes)
success_rate = backtest(close_prices, volumes, trend)
print("Success Rate:", success_rate)
plot_results(close_prices, trend)



