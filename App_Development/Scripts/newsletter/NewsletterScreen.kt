package com.tus.k00276275_project.newsletter

import androidx.compose.foundation.layout.*
import androidx.compose.foundation.text.KeyboardOptions
import androidx.compose.material3.*
import androidx.compose.runtime.*
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.input.KeyboardType
import androidx.compose.ui.unit.dp
import androidx.navigation.NavHostController

@Composable
fun NewsletterScreen(navController: NavHostController) {
    var emailInput by remember { mutableStateOf("") }
    var confirmationMessage by remember { mutableStateOf("") }

    Surface(
        modifier = Modifier
            .fillMaxSize()
            .padding(16.dp)
    ) {
        Column(
            verticalArrangement = Arrangement.Top,
            modifier = Modifier.fillMaxSize()
        ) {
            Text(
                text = "Newsletter Signup",
                style = MaterialTheme.typography.headlineMedium,
                modifier = Modifier.padding(bottom = 16.dp)
            )

            // Email Input Box
            OutlinedTextField(
                value = emailInput,
                onValueChange = { emailInput = it },
                label = { Text("Enter your email") },
                keyboardOptions = KeyboardOptions(keyboardType = KeyboardType.Email),
                modifier = Modifier
                    .fillMaxWidth()
                    .padding(bottom = 16.dp)
            )

            // Button
            Button(
                onClick = {
                    if (emailInput.isNotBlank()) {
                        confirmationMessage = "$emailInput has been added to the List"
                    } else {
                        confirmationMessage = "Please enter a valid email."
                    }
                },
                modifier = Modifier.fillMaxWidth()
            ) {
                Text("Subscribe")
            }

            Spacer(modifier = Modifier.height(16.dp))

            // Display Confirmation Message
            if (confirmationMessage.isNotEmpty()) {
                Text(
                    text = confirmationMessage,
                    style = MaterialTheme.typography.bodyLarge,
                    modifier = Modifier.padding(top = 16.dp)
                )
            }
        }
    }
}