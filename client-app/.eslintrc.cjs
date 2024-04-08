module.exports = {
    root: true,
    env: { browser: true, es2020: true },
    extends: [
        'eslint:recommended',
        'plugin:@typescript-eslint/recommended',
        'plugin:react-hooks/recommended',
    ],
    ignorePatterns: ['dist', '.eslintrc.cjs'],
    parser: '@typescript-eslint/parser',
    plugins: ['react-refresh'],
    rules: {
        'react-refresh/only-export-components': [
            'off', // disables fast refresh warning on es-lint. this warning comes up when a component updates forces react to reload and not refresh. in this instance, working with MobX class components do not support fast refresh (hmr updates)
            { allowConstantExport: true },
        ],
    },
}
