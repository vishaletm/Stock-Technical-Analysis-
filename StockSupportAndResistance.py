import pandas as pd

import numpy as np

import matplotlib.pyplot as plt

def find_support_resistance(prices):

    rolling_max = prices.rolling(window=10).max()

    resistance = rolling_max.where(rolling_max == prices, np.nan)

    resistance = resistance.dropna()

    rolling_min = prices.rolling(window=10).min()

    support = rolling_min.where(rolling_min == prices, np.nan)

    support = support.dropna()

    return support, resistance

# example usage

df = pd.read_csv("stock_prices.csv")

prices = df['Close']

support, resistance = find_support_resistance(prices)

# Plot the stock price chart

plt.plot(prices)

# Draw lines on the support and resistance levels

plt.axhline(y=support, color='green', linestyle='--')

plt.axhline(y=resistance, color='red', linestyle='--')

# Add labels and title

plt.xlabel("Date")

plt.ylabel("Price")

plt.title("Stock Price with Support and Resistance Levels")

# Show the plot

plt.show()

