package com.tus.k00276275_project.ui.components

import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.*
import androidx.compose.material.icons.outlined.*
import androidx.compose.material3.*
import androidx.compose.runtime.Composable
import androidx.compose.runtime.getValue
import androidx.navigation.NavHostController
import androidx.navigation.compose.currentBackStackEntryAsState
import androidx.navigation.NavGraph.Companion.findStartDestination
import com.tus.k00276275_project.navigation.Screen
import com.tus.k00276275_project.navigation.screens

@Composable
fun MainPageNavigationBar(navController: NavHostController) {
    val icons = mapOf(
        Screen.Home to IconGroup(
            filledIcon = Icons.Filled.Home,
            outlineIcon = Icons.Outlined.Home,
            label = "Home"
        ),
        Screen.Coping to IconGroup(
            filledIcon = Icons.Filled.Person,
            outlineIcon = Icons.Outlined.Person,
            label = "Coping"
        ),
        Screen.Helping to IconGroup(
            filledIcon = Icons.Filled.Star,
            outlineIcon = Icons.Outlined.Star,
            label = "Helping"
        ),
        Screen.Support to IconGroup(
            filledIcon = Icons.Filled.Phone,
            outlineIcon = Icons.Filled.Phone,
            label = "Support"
        )
    )

    NavigationBar {
        val navBackStackEntry by navController.currentBackStackEntryAsState()
        val currentDestination = navBackStackEntry?.destination
        screens.forEach { screen ->
            val isSelected = currentDestination?.route == screen.route
            val iconGroup = icons[screen]
            if (iconGroup != null) {
                NavigationBarItem(
                    icon = {
                        Icon(
                            imageVector = if (isSelected) iconGroup.filledIcon else iconGroup.outlineIcon,
                            contentDescription = iconGroup.label
                        )
                    },
                    label = { Text(iconGroup.label) },
                    selected = isSelected,
                    onClick = {
                        navController.navigate(screen.route) {
                            popUpTo(navController.graph.findStartDestination().id) { saveState = true }
                            launchSingleTop = true
                            restoreState = true
                        }
                    }
                )
            }
        }
    }
}