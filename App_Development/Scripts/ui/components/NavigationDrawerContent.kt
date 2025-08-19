package com.tus.k00276275_project.ui.components

import androidx.compose.foundation.layout.*
import androidx.compose.material3.*
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import androidx.navigation.NavHostController
import com.tus.k00276275_project.navigation.Screen

@Composable
fun NavigationDrawerContent(
navController: NavHostController,
onDrawerClose: () -> Unit
) {
    Column(
        modifier = Modifier
            .fillMaxSize()
            .padding(16.dp),
        verticalArrangement = Arrangement.Top
    ) {
        Text(
            text = "Nav Drawer",
            style = MaterialTheme.typography.headlineMedium,
            modifier = Modifier.padding(bottom = 12.dp)
        )

        Spacer(modifier = Modifier.height(8.dp))

        // Login Button
        TextButton(onClick = {
            navController.navigate(Screen.Login.route) { // Navigate to Login screen
                popUpTo(Screen.Home.route) { saveState = true }
                launchSingleTop = true
                restoreState = true
            }
            onDrawerClose() // Close the drawer
        })
        {
            Text(text = "Login")
        }

        Spacer(modifier = Modifier.height(8.dp))

        // Settings Button
        TextButton(onClick = {
            navController.navigate(Screen.Settings.route)
            onDrawerClose()
        }) {
            Text(text = "Settings")
        }

        Spacer(modifier = Modifier.height(8.dp))

        // Donate Button
        TextButton(onClick = {
            navController.navigate(Screen.Donate.route)
            onDrawerClose()
        }) {
            Text(text = "Donate")
        }


    }
}