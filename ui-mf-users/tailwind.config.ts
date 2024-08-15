/* eslint-env node */
module.exports = {
    content: [
        "**/src/**/*.{js,jsx,ts,tsx}",
    ],
    theme: {
        extend: {
            colors: {
                'rp-gradient': 'linear-gradient(90deg, rgba(127, 0, 255, 1) 0%, rgba(154, 59, 250, 1) 100%)',
            }
        }
    }
}
