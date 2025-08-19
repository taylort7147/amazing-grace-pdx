import api from "../api"
import {
  Combobox,
  For,
  HStack,
  Portal,
  Span,
  Spinner,
  useCombobox,
  useListCollection,
} from "@chakra-ui/react"
import { useEffect, useState } from "react"
import { useAsync } from "react-use"


export default function SeriesComboBox({ value, onValueChange }) {
  // Combobox textual input state
  const [inputValue, setInputValue] = useState("")

  // Create a list of custom items to use for the combobox.
  // Display the series name, but store the series object as the value.
  const { collection, set: setCollection } = useListCollection({
    initialItems: [],
    itemToString: (item) => item.name,
    itemToValue: (item) => item,
  });

  // Whenever the selected value changes, update the input value to match
  useEffect(() => {
    setInputValue(value?.name || "")
  }, [value]);

  // Configuration for the combobox
  const combobox = useCombobox({
    collection,
    placeholder: "Select a series",
    inputValue,
    onInputValueChange: (e) => setInputValue(e.inputValue),
    onValueChange: onValueChange,
    openOnClick: true
  })

  // Function to filter items based on input
  const filter = (items) => {
    return items.filter((item) =>
      item.name.toLowerCase().includes(inputValue.toLowerCase())
    );
  }

  // When input changes, re-fetch and filter
  const state = useAsync(async () => {
    const response = await api("/series");
    const filteredValues = filter(response.data);
    setCollection(filteredValues);
  }, [inputValue])

  return (
    <Combobox.RootProvider value={combobox}>
      <Combobox.Control>
        <Combobox.Input placeholder="Type to search" />
        <Combobox.IndicatorGroup>
          <Combobox.ClearTrigger />
          <Combobox.Trigger />
        </Combobox.IndicatorGroup>
      </Combobox.Control>

      <Portal>
        <Combobox.Positioner>
          <Combobox.Content>
            {state.loading ? (
              <HStack p="2">
                <Spinner size="xs" />
                <Span>Loading...</Span>
              </HStack>
            ) : state.error ? (
              <Span p="2" color="fg.error">
                {state.error.message}
              </Span>
            ) : (
              <For
                each={collection.items}
                fallback={<Combobox.Empty>No items</Combobox.Empty>}
              >
                {(item) => (
                  <Combobox.Item key={item.id} item={item}>
                    {item.name}
                    <Combobox.ItemIndicator />
                  </Combobox.Item>
                )}
              </For>
            )}
          </Combobox.Content>
        </Combobox.Positioner>
      </Portal>
    </Combobox.RootProvider>
  )
}
