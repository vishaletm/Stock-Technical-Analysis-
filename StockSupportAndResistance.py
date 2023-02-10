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

def plot_support_resistance(prices, support, resistance):

    fig, ax = plt.subplots()

    ax.plot(prices, label='Stock Price')

    ax.plot(support, 'g^', markersize=10, label='Support')

    ax.plot(resistance, 'rv', markersize=10, label='Resistance')

    ax.legend()

    plt.show()

def backtest(prices, support, resistance):

    signals = []

    for i in range(len(prices)):

        if prices[i] >= resistance[i]:

            signals.append(-1)

        elif prices[i] <= support[i]:

            signals.append(1)

        else:

            signals.append(0)

    signals = pd.Series(signals, index=prices.index)

    signals = signals.shift(1).dropna()

    returns = (prices - prices.shift(1)) / prices.shift(1)

    returns = returns[signals != 0]

    signals = signals[signals != 0]

    strategy_returns = returns * signals

    print("Backtesting Results:")

    print("Average daily return:", strategy_returns.mean())

    print("Standard deviation of daily returns:", strategy_returns.std())

    print("Annualized Sharpe Ratio:", strategy_returns.mean() / strategy_returns.std() * np.sqrt(252))

df = pd.read_csv("stock_prices.csv")

prices = df['Close']

support, resistance = find_support_resistance(prices)

plot_support_resistance(prices, support, resistance)

backtest(prices, support, resistance)
