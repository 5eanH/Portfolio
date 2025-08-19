package com.tus.k00276275_project.helping

import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.Surface
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import androidx.navigation.NavHostController
import com.tus.k00276275_project.ui.components.TopLevelScaffold

@Composable
fun HelpingScreen(navController: NavHostController) {
    Surface(
        modifier = Modifier
            .fillMaxSize()
            .padding(16.dp) // Padding for the content
    ) {
        Column(
            modifier = Modifier
                .fillMaxSize()
                .padding(16.dp) // Extra padding for column content
        ) {
            Text(
                text = "Tips for caregivers",
                modifier = Modifier.padding(bottom = 16.dp), // Bottom spacing for the title
                style = androidx.compose.material3.MaterialTheme.typography.headlineMedium
            )

            Text(
                text = "Not only is a diagnosis of dementia difficult for the person with the disease; it also poses significant challenges for those who take care of the patient. Family members or others caring for a person with dementia are often subject to extreme stress. They often feel isolated, alone, and left to their own devices, dealing with the \"unknown\" and seeing their beloved ones becoming more and more distant and estranged. They may develop feelings of anger, resentment, guilt, and hopelessness, in addition to the sorrow they feel for their loved one and for themselves. Depression is an extremely common consequence of being a full-time caregiver for a person with dementia. The most important thing to remember is that you cannot help someone else without helping yourself first.",
                modifier = Modifier.padding(bottom = 16.dp), // Bottom spacing for paragraphs
                style = androidx.compose.material3.MaterialTheme.typography.bodyLarge
            )

            Text(
                text = "Attend to your own physical and mental health. Research shows that Alzheimer’s Disease (AD) caregivers are often at increased risk for depression and illness, especially if they do not receive adequate support from family, friends and the community. If you are a caregiver, be sure you make time for sleep, exercise, proper nutrition, socialising, and private time. If you are run down, exhausted or socially isolated, you run the risk of becoming physically ill, overwhelmed and/or depressed. Do not hesitate to get outside help in time. The sooner you accept help, the longer you can be a partner to the person with dementia.\n" +
                        "Monitor yourself for signs of burnout (such as anger, anxiety, irritability, depression, social withdrawal, feeling extremely tired, or sleep problems), and/or health problems (such as ulcers, migraines, or high blood pressure). If necessary, seek mental health care from a professional who has expertise in addressing caregiver burnout, depression, and grief.\n" +
                        "Try joining a caregiver support group, which can provide education and emotional support. Such support group can also provide connections to local resources that can help you meet your responsibilities.",
                modifier = Modifier.padding(vertical = 8.dp), // Vertical spacing for paragraphs
                style = androidx.compose.material3.MaterialTheme.typography.bodyLarge
            )
        }
    }
}