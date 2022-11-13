import matplotlib.pyplot as plt
import pandas as pd

# df = pd.read_excel('Apollo.xlsx')
# df.head()

# df2 = pd.read_excel('Astro.xlsx')
# df2.head()
#
# df3 = pd.read_excel('Buzz.xlsx')
# df3.head()
#
df4 = pd.read_excel('Challeng.xlsx')
df4.head()

# pd.plotting.scatter_matrix(df)
# pd.plotting.scatter_matrix(df2)
# pd.plotting.scatter_matrix(df3)
pd.plotting.scatter_matrix(df4)

plt.show()