/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/**/*.{html,js}"],
  theme: {
    
      backgroundSize: {
        'auto': 'auto',
        'cover': 'cover',
        'contain': 'contain',
        '35%': '35%',
        '50%': '50%',
        '80%': '80%',
        '90%': '90%',
        '100%': '100%',
      },
      letterSpacing: {
        spaceTwo: '-.02%', 
      },
      
    extend: {
      // mailColors:{
      //   'mainRed':'#D92626',
      //   'mainGray':'#7E8285',
      //   'mainBlack':'#252525',
      // },
      lineHeight: {
        '16': '62px',
        
      }
    },
  },
  darkMode: "class",
  // corePlugins: {
  //   preflight: false,
  // },
  plugins: [],
}

