package com.tus.k00276275_project.settings

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.RadioButton
import androidx.compose.material3.Surface
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import androidx.navigation.NavHostController
import com.tus.k00276275_project.ui.components.TopLevelScaffold

@Composable
fun SettingsScreen(navController: NavHostController) {
    Column(
        modifier = Modifier
            .fillMaxSize()
            .padding(16.dp),
        verticalArrangement = Arrangement.Top
    ) {
        Text(
            text = "Settings",
            style = MaterialTheme.typography.headlineMedium,
            modifier = Modifier.padding(bottom = 24.dp)
        )

        // Font Size Section
        Text(
            text = "Change Font Size",
            style = MaterialTheme.typography.bodyLarge,
            modifier = Modifier.padding(bottom = 8.dp)
        )
        DummyRadioButtons(options = listOf("Small", "Medium", "Large"))

        Spacer(modifier = Modifier.height(24.dp))

        // Notifications Section
        Text(
            text = "Enable/Disable Notifications",
            style = MaterialTheme.typography.bodyLarge,
            modifier = Modifier.padding(bottom = 8.dp)
        )
        DummyRadioButtons(options = listOf("Enabled", "Disabled"))

        Spacer(modifier = Modifier.height(24.dp))

        // Theme Section
        Text(
            text = "Select Theme Mode",
            style = MaterialTheme.typography.bodyLarge,
            modifier = Modifier.padding(bottom = 8.dp)
        )
        DummyRadioButtons(options = listOf("Light", "Dark"))
    }
}

@Composable
fun DummyRadioButtons(options: List<String>) {
    Column {
        options.forEach { option ->
            Row(
                verticalAlignment = Alignment.CenterVertically,
                modifier = Modifier.padding(vertical = 4.dp)
            ) {
                RadioButton(
                    selected = false, // No functionality, always unselected
                    onClick = { }
                )
                Text(text = option, style = MaterialTheme.typography.bodyLarge)
            }
        }
    }
}