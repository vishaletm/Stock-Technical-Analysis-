import pandas as pd

import numpy as np

import talib

def trend_detector(close_prices):

    # Calculate the moving average convergence divergence (MACD)

    macd, signal, hist = talib.MACD(close_prices)

    

    # Calculate the relative strength index (RSI)

    rsi = talib.RSI(close_prices)

    

    # Calculate the moving average

    ma = talib.SMA(close_prices)

    

    # Combine the indicators to determine the trend

    if (macd[-1] > signal[-1]) & (rsi[-1] > 50) & (close_prices[-1] > ma[-1]):

        return "Uptrend"

    else:

        return "Downtrend"

# Example usage

close_prices = [100, 105, 110, 100, 95, 105, 115, 120, 115, 110]

print(trend_detector(close_prices))

