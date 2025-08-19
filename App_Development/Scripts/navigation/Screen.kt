package com.tus.k00276275_project.navigation

sealed class Screen(val route: String) {
    // Bottom Bar Screens
    object Home : Screen("home")
    object Coping : Screen("coping")
    object Helping : Screen("helping")
    object Support : Screen("support")

    // Drawer Screens
    object Login : Screen("login")
    object Settings : Screen("settings")
    object Donate : Screen("donate")
    object Newsletter : Screen("newsletter")
}

val screens = listOf(
    Screen.Home,
    Screen.Coping,
    Screen.Helping,
    Screen.Support
)