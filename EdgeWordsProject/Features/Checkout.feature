Feature: Checkout

Background: user is logged in and their cart is empty

@testCase2
Scenario: user checks out using valid details
	Given user adds 'Beanie' to their cart
	And fills in valid billing details:
		| name   | surname | street           | city      | postcode | phone       |
		| livija | rukmane | 0 Nowhere Street | Neverland | N0 0NN   | 01234567890 |
	When order is placed
	Then correct order appears in user's account

@testCase1
Scenario Outline: adding items to the cart
	Given user is on the main shop page
	When user adds a '<itemName>' to the cart
	Then the '<itemName>' is added to the cart 

	Examples:
	| itemName |
	| Belt     |
	| Cap      |

@testCase1
Scenario: using a valid discount code
	Given user's cart contains 'Beanie'
	And user is on the cart page
	When a discount code 'edgewords' gets submitted
	Then a discount of '15'% is applied
	And the correct total is calculated