/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,ts}",
  ],
  theme: {
    colors: {
      'primary-100': '#636BEB',
      'primary-90': '#8087EE',
      'primary-80': '#9EA3F2',
      'green': '#1DBD40',
      'yellow': '#FCD650',
      'red': '#E64646',
      'white': '#FFFFFF',
      'gray-10': '#ECEEF2',
      'gray-20': '#D2D6DE',
      'gray-30': '#A5ABB5',
      'gray-40': '#6C717A',
      'gray-60': '#4C4F56',
      'gray-70': '#303134',
      'gray-80': '#1E2023',
    },
    fontSize: {
      /* 1rem = 16px */
      'h1': ['58px', {
        lineHeight: '110%',
        letterSpacing: '0',
        fontWeight: '700',
      }],
      'h2': ['42px', {
        lineHeight: '110%',
        letterSpacing: '0',
        fontWeight: '700',
      }],
      'h3': ['34px', {
        lineHeight: '110%',
        letterSpacing: '0',
        fontWeight: '600',
      }],
      'h4': ['26px', {
        lineHeight: '120%',
        letterSpacing: '0',
        fontWeight: '500',
      }],
      'h5': ['18px', {
        lineHeight: '130%',
        letterSpacing: '0',
        fontWeight: '500',
      }],
      'body': ['1rem', {
        lineHeight: '140%',
        letterSpacing: '0',
        fontWeight: '400',
      }],
      'secondary': ['14px', {
        lineHeight: '140%',
        letterSpacing: '0',
        fontWeight: '500',
      }],
      'buttons': ['22px', {
        lineHeight: '130%',
        letterSpacing: '0',
        fontWeight: '500',
      }],
      'secondary-buttons': ['1rem', {
        lineHeight: '140%',
        letterSpacing: '0',
        fontWeight: '500',
      }],
    },
    extend: {
      'boxShadow': {
        'inner-xl': 'inset 0 0 48px 0 rgba(0, 0, 0, 0.16), inset 0 0 48px 0 rgba(0, 0, 0, 0.16)'
      }
    },
  },
  plugins: [],
}