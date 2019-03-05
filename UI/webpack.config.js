"use strict";

module.exports = {
    entry: './index.js',
    output: {
        path:'wwwroot/source/',
        filename: 'bundle.js'
    },
    watch:true,
    module: {
        loaders: [
            {
                test: /\.js$/,
                loader: "babel-loader",
                exclude: /node_modules/,
                query: {
                    presets: ["env", "react"]
                }
            }
        ]
    }
};