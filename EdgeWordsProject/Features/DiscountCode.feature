@testCase1
Feature: Discount Code

Scenario: logging in using correct details
	Given user is registered
	And user is on the account page
	When user logs in using correct log in details
	Then user gets logged in successfuly

Scenario: adding items to the cart
	Given user is on the main shop page
	When user adds a '<itemName>' to the cart
	Then the '<itemName>' is added to the cart 
	Examples:
	| itemName |
	| Belt     |
	| Cap      |

Scenario: using a valid discount code
	Given user's cart contains 'Beanie'
	And user is on the cart page
	When a discount code 'edgewords' gets submitted
	Then a discount is applied
	| percentages |
	| 10          |
	| 15          |
	And the correct total is calculated

Scenario: logging out
	Given user is on the account page
	And user is logged in
	When user logs out
	Then user gets logged out