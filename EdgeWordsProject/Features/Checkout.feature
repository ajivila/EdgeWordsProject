@testCase2
Feature: Checkout

Scenario: user checks out using valid details
	Given User is logged in
	And user adds 'Beanie' to their cart
	And fills in valid billing details
	When order is placed
	Then correct order appears in user's account