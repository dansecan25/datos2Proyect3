module.exports = {
  // ... other webpack configuration options

  // Add the resolve.fallback property
  resolve: {
    fallback: {
      url: require.resolve('url'),
    },
  },
};